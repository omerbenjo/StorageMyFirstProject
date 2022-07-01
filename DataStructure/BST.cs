using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture

{
    public class BST<T> : IEnumerable<T> where T : IComparable<T>
    {
        Node root;
        public int Counter { get; set; }
        bool isChecked;
        public bool FindBestMatch(T item, out T foundItem)
        {
            Node tmp = root;
            Node tmpHigher = tmp;
            int counter = 0;
            if (root.isChecked == false)
            {
                counter++;
                foundItem = root.value;
            }
            while (tmp != null)
            {
                if (tmp.isChecked == false)
                {

                    if (item.CompareTo(tmp.value) == 0)
                    {

                        foundItem = tmp.value;
                        tmp.isChecked = true;
                        return true;
                    }
                    if (item.CompareTo(tmp.value) >= 0)
                    {
                        tmp = tmp.right;
                        if (tmp != null)
                        {
                            if (tmp.isChecked == false)
                            {
                                tmpHigher = tmp;
                                counter++;
                            }
                        }
                    }
                    else
                    {
                        tmp = tmp.left;
                        if (tmp != null)
                        {
                            if (tmp.isChecked == false)
                            {
                                tmpHigher = tmp;
                                counter++;
                            }
                        }
                    }
                }
                else
                {
                    tmp = tmp.right;
                    if (tmp != null)
                    {
                        tmpHigher = tmp;
                        counter++;
                    }
                }

            }
            if (counter > 0)
            {
                foundItem = tmpHigher.value;
                tmpHigher.isChecked = true;
                return true;
            }
            foundItem = default;
            return false;
            // throw new Exception("no item at the range please insert a diffrent size");
        }
        public void ChangeIsChecked(T item)
        {
            Node founditem;
            Search(item, out founditem);
            founditem.isChecked = false;
        }
        private bool Search(T item, out Node foundItem)
        {
            Node tmp = root;
            while (tmp != null)
            {
                if (item.CompareTo(tmp.value) == 0)
                {
                    foundItem = tmp;
                    return true;
                }
                if (item.CompareTo(tmp.value) >= 0) tmp = tmp.right;
                else tmp = tmp.left;
            }
            foundItem = default;
            return false;
        }
        public bool Search(T item, out T foundItem)
        {
            Node tmp = root;
            while (tmp != null)
            {
                if (item.CompareTo(tmp.value) == 0)
                {
                    foundItem = tmp.value;
                    return true;
                }
                if (item.CompareTo(tmp.value) >= 0) tmp = tmp.right;
                else tmp = tmp.left;
            }
            foundItem = default;
            return false;
        }
        //public bool RemoveSingleNode(T item, out T foundItem)
        //{
        //    Node tmp = root;
        //    Node parent = null;
        //    while (tmp != null)
        //    {
        //        if (item.CompareTo(tmp.value) == 0)
        //        {
        //            if (tmp.right == null && tmp.left == null)
        //            {

        //                foundItem = tmp.value;
        //                RemoveItemWithOutChildren(parent, tmp);
        //                Counter--;
        //                return true;
        //            }
        //            else if (tmp.right == null || tmp.left == null)
        //            {
        //                foundItem = tmp.value;
        //                RemoveItemWithOneChildren(parent, tmp);
        //                Counter--;
        //                return true;
        //            }
        //            else
        //            {
        //                foundItem = tmp.value;
        //                RemoveItemWithTwoChildren(parent, tmp);
        //                Counter--;
        //                return true;
        //            }
        //        }
        //        if (item.CompareTo(tmp.value) >= 0)
        //        {
        //            parent = tmp;
        //            tmp = tmp.right;
        //        }
        //        else
        //        {
        //            parent = tmp;
        //            tmp = tmp.left;
        //        }
        //    }
        //    foundItem = default;
        //    return false;
        //}
        //private void RemoveItemWithOutChildren(Node parent, Node tmp)
        //{
        //    if (parent == null)
        //    {
        //        root = null;
        //        return;
        //    }
        //    if (parent.value.CompareTo(tmp.value) >= 0) parent.left = null;
        //    else parent.right = null;
        //}
        //private void RemoveItemWithOneChildren(Node parent, Node tmp)
        //{
        //    if (parent.left.right == null)
        //    {
        //        parent.left = null;
        //        tmp = tmp.left;
        //        parent.left = tmp;
        //        return;
        //    }
        //    else if (parent.left.left == null)
        //    {
        //        parent.left = null;
        //        tmp = tmp.right;
        //        parent.left = tmp;
        //        return;
        //    }
        //    else if (parent.right.left == null)
        //    {
        //        parent.right = null;
        //        tmp = tmp.right;
        //        parent.right = tmp;
        //        return;
        //    }
        //    else if (parent.right.right == null)
        //    {
        //        parent.right = null;
        //        tmp = tmp.left;
        //        parent.right = tmp;
        //        return;
        //    }

        //}
        //private void RemoveItemWithTwoChildren(Node parent, Node tmp)
        //{
        //    parent = tmp;
        //    tmp = tmp.left;
        //    Node tmp3 = tmp;
        //    if (tmp.left == null)
        //    {
        //        parent.value = tmp.value;
        //        if (tmp3.right == null && tmp3.left == null) RemoveItemWithOutChildren(parent, tmp);
        //        else RemoveItemWithOneChildren(parent, tmp);
        //        return;
        //    }
        //    while (true)
        //    {

        //        if (tmp.left.left == null)
        //        {
        //            tmp3 = tmp3.left;
        //            break;
        //        }
        //        tmp = tmp.left;
        //        tmp3 = tmp3.left;
        //    }
        //    parent.value = tmp3.value;
        //    if (tmp3.right == null || tmp3.left == null) RemoveItemWithOneChildren(tmp, tmp3);
        //    else RemoveItemWithOutChildren(tmp, tmp3);
        //}
        public void Add(T item) // O(logN) - O(n)
        {
            if (root == null)
            {
                root = new Node(item);
                Counter++;
                return;
            }
            Node tmp = root;
            while (true)
            {
                if (item.CompareTo(tmp.value) < 0) // item < tmp.value - go left
                {
                    if (tmp.left == null)
                    {
                        tmp.left = new Node(item);
                        break;
                    }
                    else tmp = tmp.left;
                }
                else // go right
                {
                    if (tmp.right == null)
                    {
                        tmp.right = new Node(item);
                        break;
                    }

                    else tmp = tmp.right;
                }
            }
            Counter++;
        }
        public int GetLevelsCnt()
        {
            return GetLevelsCnt(root);
        }
        int GetLevelsCnt(Node subTrreeRoot)
        {
            if (subTrreeRoot == null) return 0;

            int leftTreeDepth = GetLevelsCnt(subTrreeRoot.left);
            int rightTreeDepth = GetLevelsCnt(subTrreeRoot.right);
            return Math.Max(leftTreeDepth, rightTreeDepth) + 1;
        }
        public void ScanInOrder(Action<T> singleItemAction)  // Action<T> => void Func(T item)
        {
            ScanInOrder(root, singleItemAction);
        }
        void ScanInOrder(Node subTreeRoot, Action<T> singleItemAction)
        {
            if (subTreeRoot == null) return;

            ScanInOrder(subTreeRoot.left, singleItemAction);
            //Console.WriteLine(subTreeRoot.value);
            singleItemAction(subTreeRoot.value); //invoke
            ScanInOrder(subTreeRoot.right, singleItemAction);
        }
        bool IsEmpty()
        {
            if (root == null) return true;
            return false;
        }
        public bool RemoveSingleNode(T item)
        {
            if (IsEmpty()) return false;
            Node parentOfRemovedNode = root;
            Node removedNode = root;
            return RemoveByValue(item, parentOfRemovedNode, removedNode);
        }
        private bool RemoveByValue(T item, Node parentOfRemovedNode, Node removedNode)
        {
            if (item.CompareTo(root.value) == 0)
            {
                RemovedRoot();
                return true;
            }
            if (!SearchItem(item, out parentOfRemovedNode, out removedNode)) return false;
            if (removedNode.right == null && removedNode.left == null) // leaf
                return RemovedLeaf(item, parentOfRemovedNode);
            if (removedNode.right != null && removedNode.left != null) // 2 child
                return RemovedWithTwoChilds(item, parentOfRemovedNode, removedNode);
            return RemovedWithOneChild(item, parentOfRemovedNode, removedNode); // 1 child
        }
        private void RemovedRoot()
        {
            if (root.left == null && root.right == null) root = null;
            else if (root.left != null && root.right != null) RemovedWithTwoChilds(root.value, root, root);
            else
            {
                if (root.left == null) root = root.right;
                else root = root.left;
            }
        }
        private bool RemovedWithTwoChilds(T item, Node parentOfRemovedNode, Node removedNode)
        {
            Node tmp = removedNode.right;
            Node tmp1 = removedNode;
            parentOfRemovedNode = tmp1;
            while (tmp.left != null)
            {
                parentOfRemovedNode = tmp;
                tmp = tmp.left;
            }
            item = tmp.value;
            removedNode.value = item;
            if (tmp.right == null)
                return RemovedLeaf(item, parentOfRemovedNode);
            else
                return RemovedWithOneChild(item, parentOfRemovedNode, tmp);
        }
        private bool RemovedLeaf(T item, Node parent)
        {
            if (IsChildLeft(item, parent))
            {
                parent.left = null;
                return true;
            }
            parent.right = null;
            return true;
        }
        private bool RemovedWithOneChild(T item, Node parent, Node removedNode)
        {
            if (IsChildLeft(item, parent))
            {
                if (removedNode.right == null) parent.left = removedNode.left;
                else parent.left = removedNode.right;
                return true;
            }
            else
            {
                if (removedNode.right == null) parent.right = removedNode.left;
                else parent.right = removedNode.right;
                return true;
            }
        }
        private bool IsChildLeft(T item, Node parent)
        {
            if (item.CompareTo(parent.value) < 0)
                return true;
            return false;
        }
        private bool SearchItem(T item, out Node parent, out Node removeNode)
        {
            parent = removeNode = root;
            while (removeNode != null)
            {
                if (item.CompareTo(removeNode.value) == 0) return true;
                if (item.CompareTo(removeNode.value) < 0)
                {
                    parent = removeNode;
                    removeNode = removeNode.left;
                }
                else
                {
                    parent = removeNode;
                    removeNode = removeNode.right;
                }
            }
            return false;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return root.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        class Node : IEnumerable<T>
        {
            public T value;
            public Node left;
            public Node right;
            public bool isChecked;
            public Node(T value, bool isChecked = false)
            {
                this.isChecked = isChecked;
                this.value = value;
                left = right = null;
            }
            public IEnumerator<T> GetEnumerator()
            {
                if (left != null)
                {
                    foreach (var node in left) yield return node;
                }
                yield return value;
                if (right != null)
                {
                    foreach (var node in right) yield return node;
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}