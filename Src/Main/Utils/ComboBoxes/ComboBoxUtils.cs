using System;
using System.Drawing;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Forms.Utils.ComboBoxes
{
	/// <summary>
	/// Summary description for ComboBoxUtils.
	/// </summary>
	public class ComboBoxUtils
	{
		public ComboBoxUtils()
		{
		}

        // this is from http://rajeshkm.blogspot.com/2006/11/adjust-combobox-drop-down-list-width-c.html
        public static void SetComboScrollWidth(object sender)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.Width;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;

                //checks if a scrollbar will be displayed.
                //If yes, then get its width to adjust the size of the drop down list.
                int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

                //Loop through list items and check size of each items.
                //set the width of the drop down list to the width of the largest item.

                int newWidth;
                foreach (string s in ((ComboBox)sender).Items)
                {
                    if (s != null)
                    {
                        newWidth = (int)g.MeasureString(s.Trim(), font).Width
                        + vertScrollBarWidth;
                        if (width < newWidth)
                        {
                            width = newWidth;
                        }
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception objException)
            {
                //Catch objException
            }
        }

        public static void HighlightItemInComboBox(ComboBox comboBox, string item)
        {
            HighlightItemInComboBox(comboBox, item, true);
        }

        public static void HighlightItemInComboBox(ComboBox comboBox, string item, bool useFirstOccurance)
        {
            try
            {
                if (comboBox.Items != null && comboBox.Items.Count > 0)
                {
                    int index = -1;

                    for (int i = 0; i < comboBox.Items.Count; i++)
                    {
                        string cboItem = (string)comboBox.Items[i];
                        cboItem = cboItem.ToLower();
                        item = item.ToLower();
                        if (String.Compare(cboItem, item, true) == 0)
                        {
                            index = i;
                            if (useFirstOccurance)
                            {
                                break;
                            }
                        }
                    }

                    if (index < 0 || !useFirstOccurance)
                    {
                        for (int i = 0; i < comboBox.Items.Count; i++)
                        {
                            string cboItem = (string)comboBox.Items[i];
                            cboItem = cboItem.ToLower();
                            item = item.ToLower();
                            if (cboItem.IndexOf(item) >= 0)
                            {
                                index = i;
                                if (useFirstOccurance)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (index < 0)
                    {
                        index = 0;
                    }

                    comboBox.SelectedIndex = index;
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occured highlighting combox box item", e);
            }
        }

        public static void populateComboBox(ComboBox comboBox, string[] values)
		{
			populateComboBox(comboBox, values, false, 0);
		}

		public static void populateComboBox(ComboBox comboBox, string[] values, bool showSelection, int selectedIndex)
		{
			if (values != null)
			{
				comboBox.Items.Clear();

				for (int i=0; i<values.Length; i++)
				{
					string val = values[i];
					comboBox.Items.Add(val);
				}

				if (showSelection)
				{
					comboBox.SelectedIndex = selectedIndex;
				}
			}
		}
	}
}
