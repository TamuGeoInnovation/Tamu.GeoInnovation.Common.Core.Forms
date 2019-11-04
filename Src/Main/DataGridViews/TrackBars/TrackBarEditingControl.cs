using System;
using System.Drawing;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Forms.DataGridViews.TrackBars
{
    //  Identifies the editing control for the MaskedTextBox column type.  It
    //  isn't too much different from a regular MaskedTextBox control, 
    //  except that it implements the IDataGridViewEditingControl interface. 
    public class TrackBarEditingControl : TrackBar, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;

        public TrackBarEditingControl()
        {
            this.TickStyle = TickStyle.BottomRight;
            this.TickFrequency = 25;
        }

        //protected override void OnTextChanged(EventArgs e)
        //{
        //    base.OnTextChanged(e);
        //    // Let the DataGridView know about the value change
        //    NotifyDataGridViewOfValueChange();
        //}

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            // Let the DataGridView know about the value change
            NotifyDataGridViewOfValueChange();
        }

        //  Notify DataGridView that the value has changed.
        protected virtual void NotifyDataGridViewOfValueChange()
        {
            this.valueChanged = true;
            if (this.dataGridView != null)
            {
                this.dataGridView.NotifyCurrentCellDirty(true);
            }
        }

        #region IDataGridViewEditingControl Members

        //  Indicates the cursor that should be shown when the user hovers their
        //  mouse over this cell when the editing control is shown.
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }


        //  Returns or sets the parent DataGridView.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }

            set
            {
                this.dataGridView = value;
            }
        }


        //  Sets/Gets the formatted value contents of this cell.
        public object EditingControlFormattedValue
        {
            set
            {
                //this.Text = value.ToString();
                this.Text = Convert.ToString(this.Value);
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                //return this.Text;
                return Convert.ToString(this.Value);
            }

        }

        //   Get the value of the editing control for formatting.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            //return this.Text;
            return Convert.ToString(this.Value);
        }

        //  Process input key and determine if the key should be used for the editing control
        //  or allowed to be processed by the grid. Handle cursor movement keys for the MaskedTextBox
        //  control; otherwise if the DataGridView doesn't want the input key then let the editing control handle it.
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    //
                    // If the end of the selection is at the end of the string
                    // let the DataGridView treat the key message
                    //
                    if (this.Value >= this.Maximum)
                    {
                        return true;
                    }
                    break;

                case Keys.Left:
                    //
                    // If the end of the selection is at the begining of the
                    // string or if the entire text is selected send this character 
                    // to the dataGridView; else process the key event.
                    //
                    if (this.Value <= this.Minimum)
                    {
                        return true;
                    }
                    break;

                case Keys.Home:
                case Keys.End:
                case Keys.Prior:
                case Keys.Next:
                case Keys.Delete:
                    break;
            }

            //
            // defer to the DataGridView and see if it wants it.
            //
            return !dataGridViewWantsInputKey;
        }


        //  Prepare the editing control for edit.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                //SelectAll();
            }
            else
            {
                //
                // Do not select all the text, but position the caret at the 
                // end of the text.
                //
                //this.SelectionStart = this.ToString().Length;
            }
        }

        //  Indicates whether or not the parent DataGridView control should
        //  reposition the editing control every time value change is indicated.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return true;
            }
        }


        //  Indicates the row index of this cell.  This is often -1 for the
        //  template cell, but for other cells, might actually have a value
        //  greater than or equal to zero.
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }

            set
            {
                this.rowIndex = value;
            }
        }



        //  Make the control match the style and colors of
        //  the host DataGridView control and other editing controls 
        //  before showing the editing control.
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            //this.Font = dataGridViewCellStyle.Font;
            this.Font = new Font("Microsoft Sans Serif", 6.25F);
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
            //this.TextAlign = translateAlignment(dataGridViewCellStyle.Alignment);
        }


        //  Gets or sets our flag indicating whether the value has changed.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }

            set
            {
                this.valueChanged = value;
            }
        }

        #endregion // IDataGridViewEditingControl.

        ///   Routine to translate between DataGridView
        ///   content alignments and text box horizontal alignments.
        private static HorizontalAlignment translateAlignment(DataGridViewContentAlignment align)
        {
            switch (align)
            {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.MiddleLeft:
                case DataGridViewContentAlignment.BottomLeft:
                    return HorizontalAlignment.Left;

                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.BottomCenter:
                    return HorizontalAlignment.Center;

                case DataGridViewContentAlignment.TopRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.BottomRight:
                    return HorizontalAlignment.Right;
            }

            throw new ArgumentException("Error: Invalid Content Alignment!");
        }

    }
}
