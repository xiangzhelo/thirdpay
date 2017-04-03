using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomController
    {
        //待随机抽取数据集合
        public List<int> datas = new List<int>(
            new int[]{});

        //权值
        public List<ushort> weights = new List<ushort>(
            new ushort[]{
            1,2,3,4,5,6,
            7,8,9,0,1,1,
            1,1,1,1,1,1,
            1,1,1,1,1,1,
            1,1
    });

        #region Properties

        private int _Count;
        /// <summary>
        /// 随机抽取个数
        /// </summary>
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }

        #endregion

        #region Contructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="count">随机抽取个数</param>
        public RandomController(ushort count)
        {
            if (count > 26)
                throw new Exception("抽取个数不能超过数据集合大小！！");

            _Count = count;
        }

        #endregion

        /// <summary>
        /// 排序集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private List<KeyValuePair<int, int>> SortByValue(Dictionary<int, int> dict)
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

            if (dict != null)
            {
                list.AddRange(dict);

                list.Sort(
                  delegate(KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
                  {
                      return kvp2.Value - kvp1.Value;
                  });
            }
            return list;
        }

        #region 受控随机抽取

        /// <summary>
        /// 随机抽取
        /// </summary>
        /// <param name="rand">随机数生成器</param>
        /// <returns></returns>
        public int[] ControllerRandomExtract(Random rand)
        {
            List<int> result = new List<int>();
            if (rand != null)
            {
                //临时变量
                Dictionary<int, int> dict = new Dictionary<int, int>(26);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dict.Add(datas[i], rand.Next(1000) * weights[i]);
                }

                //排序
                List<KeyValuePair<int, int>> listDict = SortByValue(dict);

                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<int, int> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result.ToArray();
        }

        #endregion

        #region 普通随机抽取

        /// <summary>
        /// 随机抽取
        /// </summary>
        /// <param name="rand">随机数生成器</param>
        /// <returns></returns>
        public int[] RandomExtract(Random rand)
        {
            List<int> result = new List<int>();
            if (rand != null)
            {
                for (int i = Count; i > 0; )
                {
                    int item = datas[rand.Next(25)];
                    if (result.Contains(item))
                        continue;
                    else
                    {
                        result.Add(item);
                        i--;
                    }
                }
            }
            return result.ToArray();
        }

        #endregion
    }
}