using System.Collections;
using System.Windows.Forms;
using USC.GISResearchLab.Common.Utils.ArrayLists;


namespace USC.GISResearchLab.Common.Forms.Utils.CheckedListBoxes
{
	/// <summary>
	/// Summary description for CheckedListBoxUtils.
	/// </summary>
	public class CheckedListBoxUtils
	{
		public CheckedListBoxUtils()
		{
		}

		public static ArrayList asList(CheckedListBox checkedListBox, bool ignoreFirstItem)
		{
			ArrayList ret = new ArrayList();

			if (checkedListBox != null)
			{
				int numItems = checkedListBox.Items.Count;

				for (int i=0; i<numItems; i++)
				{
					bool isSelected = checkedListBox.GetItemChecked(i);

					if (isSelected)
					{
						string val = checkedListBox.Items[i].ToString();
						if (i==0)
						{
							if (!ignoreFirstItem)
							{
								ret.Add(val);
							}
						}
						else
						{
							ret.Add(val);
						}
					}

				}
			}
			return ret;
		}

		public static void populateList(CheckedListBox checkedListBox, ArrayList values, ArrayList checkedValues, bool includeSelectAll)
		{
			if (values != null)
			{
				checkedListBox.Items.Clear();

				if (includeSelectAll)
				{
					checkedListBox.Items.Add("Select All");
				}

				for (int i=0; i<values.Count; i++)
				{
					string val = (string) values[i];
					checkedListBox.Items.Add(val);

					if (checkedValues != null)
					{
						if (ArrayListUtils.ContainsString(checkedValues, val))
						{
							int curr = i;
							if (includeSelectAll)
							{
								curr ++;
							}

							checkedListBox.SetItemChecked(curr, true);
						}
					}
				}

				if (hasAllSelected(checkedListBox))
				{
					if (includeSelectAll)
					{
						checkedListBox.SetItemChecked(0, true);
					}
				}
			}
		}

		public static bool hasAllSelected(CheckedListBox checkedListBox)
		{
			return hasAllSelected(checkedListBox, true);
		}

		public static bool hasAllSelected(CheckedListBox checkedListBox, bool ignoreFirstItem)
		{
			bool ret = true;
			if (checkedListBox != null)
			{
				int numItems = checkedListBox.Items.Count;

				for (int i=0; i<numItems; i++)
				{
					bool isSelected = checkedListBox.GetItemChecked(i);

					if (i==0)
					{
						if (!ignoreFirstItem)
						{
							ret = ret && isSelected;
						}
					}
					else
					{
						ret = ret && isSelected;
					}

				}
			}
			return ret;
		}
	}
}
