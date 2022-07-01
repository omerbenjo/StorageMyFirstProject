using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{
    public class LinkedList1060<T> : IEnumerable<T>
    {
        private Node first = null;
        private Node last = null;
        private int count;

        public void AddFirst(T value) // O(1)
        {
            Node n = new Node(value);
            if (first == null) last = n;
            n.Next = first;
            first = n;
            count++;
        }

        public void AddLast(T value) // O(1)
        {
            if (first == null)
            {
                AddFirst(value);
                return;
            }
            Node n = new Node(value);
            last.Next = n;
            last = n;
            count++;
        }

        public void RemoveLast() // O(n)
        {

        }

        public void RemoveFirst() // O(1)
        {
            if (last == first)
            {
                first = first.Next;// first = null;
                last = null;
                return;
            }
            first = first.Next;
            count--;
        }
        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }
        public bool RemoveFirst(out T removedValue) // O(1)
        {
            removedValue = default;
            if (first == null) return false;

            removedValue = first.Value;
            first = first.Next;
            count--;
            return true;
        }

        //Zero based index
        public bool GetAt(int index, out T foundValue) //O(n)
        {
            foundValue = default;
            if (index >= count || index < 0) return false;

            Node tmp = first;
            for (int i = 0; i < index; i++) tmp = tmp.Next;

            foundValue = tmp.Value;
            return true;
        }

        public override string ToString() // O(n)
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = first;

            while (tmp != null)
            {
                sb.Append($"{tmp.Value}  ");
                tmp = tmp.Next;
            }
            return sb.ToString();
        }

        //public IEnumerator<T> GetEnumerator()
        //{
        //    EnumeratorItemInList listEnum = new EnumeratorItemInList(first);
        //    return listEnum;
        //}

        public IEnumerator<T> GetEnumerator()
        {
            Node tmp = first;
            while (tmp != null)
            {
                yield return tmp.Value;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        //class EnumeratorItemInList : IEnumerator<T>
        //{
        //    Node tmp;
        //    bool isFirstTime;

        //    public EnumeratorItemInList(Node start)
        //    {
        //        isFirstTime = true;
        //        tmp = start;
        //    }

        //    public T Current
        //    {
        //        get { return tmp.Value; }
        //    }

        //    object IEnumerator.Current => throw new NotImplementedException();

        //    public void Dispose()
        //    {

        //    }

        //    public bool MoveNext()
        //    {
        //        if (tmp == null) return false;

        //        if (isFirstTime)
        //        {
        //            isFirstTime = false;
        //            return true;
        //        }

        //        tmp = tmp.Next;
        //        return tmp != null;                
        //    }

        //    public void Reset()
        //    {

        //    }

        //    public void Func1()
        //    {

        //    }
        //}

        class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }

            public Node(T value)
            {
                this.Value = value;
                Next = null;
            }
        }
    }
}

