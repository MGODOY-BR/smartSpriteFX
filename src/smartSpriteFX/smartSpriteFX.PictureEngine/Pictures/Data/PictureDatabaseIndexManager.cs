using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Controls the index database manager
    /// </summary>
    [Obsolete("This class was created for evaluation only and can't be used yet", true)]
    public class PictureDatabaseIndexManager
    {
        /// <summary>
        /// It's the underline index reference
        /// </summary>
        public Dictionary<string, int> UnderlineIndex { get; private set; }

        /// <summary>
        /// It's the under
        /// </summary>
        public IList UnderlineData { get; private set; }

        public PictureDatabaseIndexManager(IList underlineData, Dictionary<string, int> underlineIndex)
        {
            this.UnderlineIndex = underlineIndex;
            this.UnderlineData = underlineData;
        }

        /// <summary>
        /// Enlist an index.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        public void EnlistIndex(string key, int index)
        {
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(delegate (object state)
                {
                    lock(this)
                    {
                        object[] stateArray = (object[])state;
                        this.UnderlineIndex.Add((string)stateArray[0], (int)stateArray[1]);
                    }
                }),
                new object[2] { key, index });
        }

        /// <summary>
        /// Synchronizes the calling to feed of index
        /// </summary>
        /// <returns></returns>
        public void WaitForCompletion()
        {
            while(this.UnderlineIndex.Count < this.UnderlineData.Count)
            {
                Thread.Sleep(1);
            }
        }
    }
}
