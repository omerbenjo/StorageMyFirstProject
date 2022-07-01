using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{
    public class DoublyLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node
        {
            public T data;
            internal Node previous;
            internal Node next;
            public Node(T data)
            {
                this.data = data;
                next = null;
                previous = null;
            }
        }
        Node first = null;
        Node last = null;
        int counter;
        public int Counter
        {
            get { return counter; }
            private set { counter = value; }
        }

        public void UpdateExpiredList(Node node)
        {
            Node tmpLeft = node.previous;
            Node tmpRight = node.next;

            if (tmpRight == null) return;// item is alreardy last
            if (tmpLeft == null)
            {
                first = first.next;
                first.previous = null;
            }
            else
            {
                tmpRight.previous = tmpLeft;
                tmpLeft.next = tmpRight;
            }
            last.next = node;
            node.previous = last;
            node.next = null;
            last = last.next;
        }
        public void RemoveExpiredList(Node node)
        {
            Node tmpleft = node.previous;
            Node tmpright = node.next;
            if (tmpleft == null)
            {
                RemoveFirst();
                return;
            }
            if (tmpright == null)
            {
                RemoveLast();
                return;
            }
            tmpright.previous = tmpleft;
            tmpleft.next = tmpright;
        }
        public void AddLast(Node node)
        {
            if (first == null)
            {
                AddFirst(node);
                return;
            }
            last.next = node;
            node.previous = last;
            last = node;
            counter++;
        }
        public void AddFirst(Node node)
        {
            if (first == null)
            {
                first = node;
                last = node;
                counter++;
                return;
            }
            first.previous = node;
            node.next = first;
            first = node;
            counter++;
        }
        public void AddFirst(T data)
        {
            Node n = new Node(data);
            if (first == null)
            {
                first = n;
                last = n;
                counter++;
                return;
            }
            first.previous = n;
            n.next = first;
            first = n;
            counter++;
        }
        public void AddLast(T data)
        {
            if (first == null)
            {
                AddFirst(data);
                return;
            }
            Node n = new Node(data);
            last.next = n;
            n.previous = last;
            last = n;
            counter++;
        }
        public void RemoveFirst()
        {
            if (last == first)
            {
                first = first.next;// first = null;
                last = null;
                counter--;
                return;
            }
            first = first.next;
            counter--;

        }
        public void RemoveLast()
        {
            if (last == first)
            {
                first = first.next;// first = null;
                last = null;
                return;
            }
            last = last.previous;
            last.next = null;

            counter--;
        }
        public bool GetAt(int position, out T value)
        {
            if (counter == 0 || position < 0 || position > counter)
            {
                value = default;
                return false;
            }
            Node n = first;
            for (int i = 0; i < position; i++)
            {
                n = n.next;
            }
            value = n.data;
            return true;
        }
        public bool AddAt(int position, T data)
        {
            Node n = first;
            if (position >= counter)
            {
                return false;
            }
            for (int i = 0; i <= position; i++)
            {
                if (i == position)
                {
                    Node newN = new Node(data);
                    newN.previous = n.previous;
                    newN.next = n;
                    newN.previous.next = newN;
                    newN.next.previous = newN;
                    return true;
                }
                n = n.next;
            }
            return false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = first;

            while (tmp != null)
            {
                sb.Append($"{tmp.data}  ");
                tmp = tmp.next;
            }
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node tmp = first;
            while (tmp != null)
            {
                yield return tmp.data;
                tmp = tmp.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
