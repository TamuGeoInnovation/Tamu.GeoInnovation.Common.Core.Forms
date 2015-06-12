using System;
using System.Drawing;
using System.Windows.Forms;

// Ref: http://stackoverflow.com/questions/3529928/how-do-i-put-text-on-progressbar
// Copyed by Kaveh on Feb 29, 2012

namespace USC.GISResearchLab.Common.Core.Forms
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }

    public class LabeledProgressBar : ProgressBar
    {
        //Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText CustomDisplayStyle { get; set; }

        // This is the label color for the brush
        public Color LabelColor { get; set; }

        //Property to hold the custom text
        public String CustomText { get; set; }

        public LabeledProgressBar() : base()
        {
            // Modify the ControlStyles flags
            //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CustomText = string.Empty;
            CustomDisplayStyle = ProgressBarDisplayText.Percentage;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;
            int value = (100 * (Value - Minimum)) / (Maximum - Minimum);

            // Set the Display text (Either a % amount or our custom text
            string text = CustomDisplayStyle == ProgressBarDisplayText.Percentage ? value.ToString() + '%' : CustomText;

            SizeF len = g.MeasureString(text, Font);
            // Calculate the location of the text (the middle of progress bar)
            Point location = new Point(Convert.ToInt32((rect.Width - len.Width) / 2), 3 + Convert.ToInt32((rect.Height - len.Height) / 2));

            // draw the outter box
            if (ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(g, rect);
                rect.Inflate(-3, -3);

                // draw the green rectangle
                if (value > 0)
                {
                    // As we doing this ourselves we need to draw the chunks on the progress bar
                    Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)value / (Maximum - Minimum)) * rect.Width), rect.Height);
                    ProgressBarRenderer.DrawHorizontalChunks(g, clip);
                }
            }
            else
            {
                g.FillRectangle(new SolidBrush(BackColor), rect);
                rect.Inflate(-3, -3);

                // draw the green rectangle
                if (value > 0)
                {
                    // As we doing this ourselves we need to draw the chunks on the progress bar
                    Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)value / (Maximum - Minimum)) * rect.Width), rect.Height);
                    g.FillRectangle(new SolidBrush(ForeColor), clip);
                }
            }
            // Draw the custom text           
            g.DrawString(text, Font, new SolidBrush(LabelColor), location);
        }

        public string EstimateRemainingTime(DateTime start, double percent)
        {
            string sentense = string.Empty;
            if (percent >= Minimum && percent < Maximum)
            {
                double passedMin = (DateTime.Now - start).TotalMinutes;
                if ((percent < 3.0) && (passedMin < 4.0)) sentense = "estimating time ...";
                else
                {
                    int remainMin = Convert.ToInt32(passedMin / percent * (100 - percent));
                    if (remainMin < 1) sentense = "Almost Done";
                    else
                    {
                        if (remainMin < 60)
                            sentense = "Remain: " + remainMin + " min(s)";
                        else if (remainMin < 1440)
                            sentense = "Remain: " + (remainMin / 60) + " hour(s), " + (((remainMin % 60) / 5) * 5) + " min(s)";
                        else
                            sentense = "Remain: " + (remainMin / 1440) + " day(s), " + ((remainMin % 1440) / 60) + " hour(s)";
                    }
                }
            }
            return sentense;
        }
    }
}
