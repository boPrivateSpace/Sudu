using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    class SuDu
    {
        public SuDu()
        {
        }
        public static int[][] arrray1;
        public static int[][] getSuDu()
        {
            arrray1 = new int[9][];
            arrray1[0] = new int[9] { 5, 6, 4, 8, 9, 7, 2, 3, 1 };
            arrray1[1] = new int[9] { 9, 7, 8, 3, 1, 2, 6, 4, 5 };
            arrray1[2] = new int[9] { 3, 1, 2, 6, 4, 5, 9, 7, 8 };
            arrray1[3] = new int[9] { 6, 4, 5, 9, 7, 8, 3, 1, 2 };
            arrray1[4] = new int[9] { 7, 8, 9, 1, 2, 3, 4, 5, 6 };
            arrray1[5] = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            arrray1[6] = new int[9] { 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            arrray1[7] = new int[9] { 8, 9, 7, 2, 3, 1, 5, 6, 4 };
            arrray1[8] = new int[9] { 2, 3, 1, 5, 6, 4, 8, 9, 7 };
            
            List<int> randomList = creatNineRondomArray(9);
            int[][] result = creatSudokuArray(arrray1, randomList);
            printArray(result);
            return result;
        }


        /// <summary>
        /// 打印二维数组，数独矩阵
        /// </summary>
        /// <param name="a"></param>
        private static void printArray(int[][] a)
        {
            string charss = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                     charss += a[i][j] + "  ";
                }
            }
            Write(charss);
        }

        /// <summary>
        /// 产生一个1-9的不重复长度为9的一维数组 
        /// </summary>
        /// <returns></returns>
        public static List<int> creatNineRondomArray(int length)
        {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {

                int randomNum = random.Next(9) + 1;
                while (true)
                {
                    if (!list.Contains(randomNum))
                    {
                        list.Add(randomNum);
                        break;
                    }
                    randomNum = random.Next(9) + 1;
                }

            }
            return list;
        }

        /// <summary>
        ///	 通过一维数组和原数组生成随机的数独矩阵遍历二维数组里的数据，在一维数组找到当前值的位置，并把一维数组   
        ///当前位置加一处位置的值赋到当前二维数组中。目的就是将一维数组为
        ///依据，按照随机产生的顺序，将这个9个数据进行循环交换，生成一个随机的数独矩阵。
        /// </summary>
        /// <param name="seedArray"></param>
        /// <param name="randomList"></param>
        private static int[][] creatSudokuArray(int[][] seedArray, List<int> randomList)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (seedArray[i][j] == randomList[k])
                        {
                            seedArray[i][j] = randomList[(k + 1) % 9];
                            break;
                        }
                    }
                }
            }

            return seedArray;
        }

        public static void Write(String s)
        {
            FileStream fs = new FileStream("E:\\ak.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入

            sw.Write(s);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
