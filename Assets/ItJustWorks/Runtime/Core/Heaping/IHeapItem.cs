using System;

namespace ItJustWorks.Heaping
{
	public interface IHeapItem<HEAPED> : IComparable<HEAPED>
	{
		public int HeapIndex { get; set; }
	}
}