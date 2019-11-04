using System;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Forms.DataGridViews.TrackBars
{
    //  The base object for the custom column type.  Programmers manipulate
    //  the column types most often when working with the DataGridView, and
    //  this one sets the basics and Cell Template values controlling the
    //  default behaviour for cells of this column type.
    public class TrackBarColumn : DataGridViewColumn
    {
        private int minValue;
        private int maxValue;
        private int currentValue;

        //  Initializes a new instance of this class, making sure to pass
        //  to its base constructor an instance of a TrackBarCell 
        //  class to use as the basic template.
        public TrackBarColumn() : base(new TrackBarCell())
        {
        }


        //  The template cell that will be used for this column by default,
        //  unless a specific cell is set for a particular row.
        //
        //  A TrackBarCell cell which will serve as the template cell
        //  for this column.
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }

            set
            {
                //  Only cell types that derive from TrackBarCell are supported as the cell template.
                if (value != null && !value.GetType().IsAssignableFrom(typeof(TrackBarCell)))
                {
                    string s = "Cell type is not based upon the TrackBarCell.";//CustomColumnMain.GetResourceManager().GetString("excNotMaskedTextBox");
                    throw new InvalidCastException(s);
                }

                base.CellTemplate = value;
            }
        }

        public virtual int MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                TrackBarCell trackBarCell;
                DataGridViewCell dataGridViewCell;
                int rowCount;

                if (this.minValue != value)
                {
                    this.minValue = value;

                    //
                    // first, update the value on the template cell.
                    //
                    trackBarCell = (TrackBarCell)this.CellTemplate;
                    trackBarCell.MinValue = value;

                    //
                    // now set it on all cells in other rows as well.
                    //
                    if (this.DataGridView != null && this.DataGridView.Rows != null)
                    {
                        rowCount = this.DataGridView.Rows.Count;
                        for (int x = 0; x < rowCount; x++)
                        {
                            dataGridViewCell = this.DataGridView.Rows.SharedRow(x).Cells[x];
                            if (dataGridViewCell is TrackBarCell)
                            {
                                trackBarCell = (TrackBarCell)dataGridViewCell;
                                trackBarCell.MinValue = value;
                            }
                        }
                    }
                }
            }
        }

        public virtual int MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                TrackBarCell trackBarCell;
                DataGridViewCell dataGridViewCell;
                int rowCount;

                if (this.maxValue != value)
                {
                    this.maxValue = value;

                    //
                    // first, update the value on the template cell.
                    //
                    trackBarCell = (TrackBarCell)this.CellTemplate;
                    trackBarCell.MaxValue = value;

                    //
                    // now set it on all cells in other rows as well.
                    //
                    if (this.DataGridView != null && this.DataGridView.Rows != null)
                    {
                        rowCount = this.DataGridView.Rows.Count;
                        for (int x = 0; x < rowCount; x++)
                        {
                            dataGridViewCell = this.DataGridView.Rows.SharedRow(x).Cells[x];
                            if (dataGridViewCell is TrackBarCell)
                            {
                                trackBarCell = (TrackBarCell)dataGridViewCell;
                                trackBarCell.MaxValue = value;
                            }
                        }
                    }
                }
            }
        }

        public virtual int CurrentValue
        {
            get
            {
                return this.currentValue;
            }
            set
            {
                TrackBarCell trackBarCell;
                DataGridViewCell dataGridViewCell;
                int rowCount;

                if (this.currentValue != value)
                {
                    this.currentValue = value;

                    //
                    // first, update the value on the template cell.
                    //
                    trackBarCell = (TrackBarCell)this.CellTemplate;
                    trackBarCell.CurrentValue = value;

                    //
                    // now set it on all cells in other rows as well.
                    //
                    if (this.DataGridView != null && this.DataGridView.Rows != null)
                    {
                        rowCount = this.DataGridView.Rows.Count;
                        for (int x = 0; x < rowCount; x++)
                        {
                            dataGridViewCell = this.DataGridView.Rows.SharedRow(x).Cells[x];
                            if (dataGridViewCell is TrackBarCell)
                            {
                                trackBarCell = (TrackBarCell)dataGridViewCell;
                                trackBarCell.CurrentValue = value;
                            }
                        }
                    }
                }
            }
        }



    }
}
