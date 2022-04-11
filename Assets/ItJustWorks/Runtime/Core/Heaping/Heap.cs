namespace ItJustWorks.Heaping
{
	// This'll be HEAPS of fun
	// You'll really learn HEAPS of stuff about A*
	public class Heap<HEAPED> where HEAPED : IHeapItem<HEAPED>
	{
		public int Count { get; private set; }
		private readonly HEAPED[] items;

		public Heap(int _maxSize) => items = new HEAPED[_maxSize];

		public bool Contains(HEAPED _item) => Equals(items[_item.HeapIndex], _item);

		public void UpdateItem(HEAPED _item) => SortUp(_item);

		public void Add(HEAPED _item)
		{
			_item.HeapIndex = Count;
			items[Count] = _item;
			
			SortUp(_item);
			Count++;
		}

		public HEAPED RemoveFirst()
		{
			// Copy the first item and decrement count
			HEAPED firstItem = items[0];
			Count--;

			items[0] = items[Count];
			items[0].HeapIndex = 0;
			
			SortDown(items[0]);

			return firstItem;
		}

		private void SortDown(HEAPED _item)
		{
			while(true)
			{
				int childIndexLeft = _item.HeapIndex * 2 + 1;
				int childIndexRight = _item.HeapIndex * 2 + 2;

				// Is the current item at the end of the array?
				if(childIndexLeft < Count)
				{
					// It's not so attempt to sort it
					int swapIndex = childIndexLeft;
					
					if(childIndexRight < Count)
					{
						if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
						{
							swapIndex = childIndexRight;
						}
					}

					if(_item.CompareTo(items[swapIndex]) < 0)
					{
						Swap(_item, items[swapIndex]);
					}
					else
					{
						return;
					}
				}
				else
				{
					// Yes, so we are done sorting
					return;
				}
			}
		}

		private void SortUp(HEAPED _item)
		{
			int parentIndex = (_item.HeapIndex - 1) / 2;

			while(true)
			{
				HEAPED parent = items[parentIndex];
				// Check if the parent is greater than us
				if(_item.CompareTo(parent) > 0)
				{
					// it is, so swap them
					Swap(_item, parent);
				}
				else
				{
					// it isn't, so finish the function
					break;
				}
			}
		}

		private void Swap(HEAPED _itemA, HEAPED _itemB)
		{
			items[_itemA.HeapIndex] = _itemB;
			items[_itemB.HeapIndex] = _itemA;

			int itemAIndex = _itemA.HeapIndex;
			_itemA.HeapIndex = _itemB.HeapIndex;
			_itemB.HeapIndex = itemAIndex;
		}
	}
}