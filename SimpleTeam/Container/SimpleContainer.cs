namespace SimpleTeam.Container
{
    public abstract class SimpleContainer
		: IContainer
	{
		/**
		 * Count of object in a container.
		 */
		private int count;
		/**
		 * Capacity of a container.
		 */
		private int capacity;

		/**
		 * Size of a container.
		 */
		private int size;

		public SimpleContainer()
		{
			//Set default capacity.
			capacity = IContainer.WITHOUT_LIMIT;
		}

		public SimpleContainer(int capacity)
		{
			//Set capacity.
			this.capacity = capacity < 0 ? IContainer.WITHOUT_LIMIT : capacity;
		}

		protected void SetCount(int count)
		{
			//Set count.
			this.count = count;
		}

		protected void IncreaseCount() { count ++; }

		protected void IncreaseCount(int count) { this.count += count; }

		protected void DecreaseCount() { count --; }

		protected void DecreaseCount(int count) { this.count -= count; }

		protected void SetSize(int size)
		{
			//Set size.
			this.size = size;
		}

		protected void IncreaseSize() { size ++; }

		protected void IncreaseSize(int size) { this.size += size; }

		protected void DecreaseSize() { size --; }

		protected void DecreaseSize(int size) { this.size -= size; }

		protected void IncreaseSizeAndCount() { size ++; count ++; }

		protected void DecreaseSizeAndCount() { size --; count --; }

		public int GetSize() { return size; }

		public int GetCount() { return count; }

	    public int GetCapacity() { return capacity; }

		public void SetCapacity(int capacity)
		{
			//Set capacity.
			this.capacity = capacity < 0 ? IContainer.WITHOUT_LIMIT : capacity;
		}

		public bool IsEmpty() { return count <= 0; }

		public bool IsFull() { return capacity >= 0 && count >= capacity; }

		public void ClearAll() { size = 0; count = 0; }
	}
}
