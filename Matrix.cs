using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer
{
    class Matrix3x3
    {
        public List<List<decimal>> MatrixData { get; private set; }

        public decimal Determinant
        {
            get =>
            MatrixData[0][0] * MatrixData[1][1] * MatrixData[2][2] +
            MatrixData[0][1] * MatrixData[2][0] * MatrixData[1][2] +
            MatrixData[1][0] * MatrixData[0][2] * MatrixData[2][1] -
            MatrixData[2][0] * MatrixData[1][1] * MatrixData[0][2] -
            MatrixData[0][1] * MatrixData[2][2] * MatrixData[1][0] -
            MatrixData[0][0] * MatrixData[1][2] * MatrixData[2][1];
        }


        public Matrix3x3(List<int> items)
        {
            MatrixData = new List<List<decimal>>();

            byte sideSize = 3;

            for (byte i = 0; i < sideSize; ++i)
            {
                MatrixData.Add(new List<decimal>());
                int rowIndex = i * sideSize;

                for (byte j = 0; j < sideSize; ++j)
                {
                    MatrixData[i].Add(items[rowIndex + j]);
                }
            }
        }


        public void ReplaceColumn(byte columnIndex, List<int> newColumn)
        {
            if (newColumn.Count > 3) throw new IndexOutOfRangeException("Maximum length [newColumn] - 3.");

            MatrixData[0][columnIndex] = newColumn[0];
            MatrixData[1][columnIndex] = newColumn[1];
            MatrixData[2][columnIndex] = newColumn[2];
        }
    }
}
