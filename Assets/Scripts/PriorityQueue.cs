using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//visualstudiomagazine.com/Articles/2012/11/01/Priority-Queues-with-C.aspx?Page=1
public class PriorityQueue <T> where T : IComparable<T>
{
    private List<T> m_Data;

	public PriorityQueue()
	{
		m_Data = new List<T>();
	}

	public void Enqueue(T _item)
	{
		m_Data.Add(_item);
		int childIndex = m_Data.Count - 1;
		while (childIndex > 0)
		{
			int parentIndex = (childIndex - 1) / 2;
			if (m_Data[childIndex].CompareTo(m_Data[parentIndex]) >= 0)
				break;

			T tmp = m_Data[childIndex];
			m_Data[childIndex] = m_Data[parentIndex];
			m_Data[parentIndex] = tmp;

			childIndex = parentIndex;
		}
	}

	public T Dequeue()
	{
		int lastIndex = m_Data.Count - 1;

		T frontItem = m_Data[0];

		m_Data[0] = m_Data[lastIndex];
		m_Data.RemoveAt(lastIndex);

		lastIndex--;

		int parentIndex = 0;
		while (true)
		{
			int childIndex = parentIndex * 2 + 1;

			if (childIndex > lastIndex)
			{
				break;
			}

			int rightChild = childIndex + 1;
			if (rightChild <= lastIndex && m_Data[rightChild].CompareTo(m_Data[childIndex]) < 0)
			{
				childIndex = rightChild;
			}

			if (m_Data[parentIndex].CompareTo(m_Data[childIndex]) < 0)
			{
				break;
			}
			T tmp = m_Data[parentIndex];
			m_Data[parentIndex] = m_Data[childIndex];
			m_Data[childIndex] = tmp;

			parentIndex = childIndex;
		}
		return frontItem;
	}

	public T Peek()
	{
		return m_Data[0];
	}

	public bool Contains(T _item)
	{
		return m_Data.Contains(_item);
	}

	public List<T> ToList()
	{
		return m_Data;
	}

	public int Count ()
	{
		return m_Data.Count	;
	}

	public void Clear()
	{
		m_Data.Clear();
	}

}
