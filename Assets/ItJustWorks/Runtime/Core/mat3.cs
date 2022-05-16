using UnityEngine;

namespace ItJustWorks
{
	public class mat3
	{
		private readonly float[,] matrix = new float[3,3];

		public float[] this[int _i]
		{
			get
			{
				return new[] {matrix[0, _i], matrix[1, _i], matrix[2, _i]};
			}
		}

		public static mat3 Rotate(Quaternion _rotation)
		{
			float num1 = _rotation.x * 2f;
			float num2 = _rotation.y * 2f;
			float num3 = _rotation.z * 2f;
			float num4 = _rotation.x * num1;
			float num5 = _rotation.y * num2;
			float num6 = _rotation.z * num3;
			float num7 = _rotation.x * num2;
			float num8 = _rotation.x * num3;
			float num9 = _rotation.y * num3;
			float num10 = _rotation.w * num1;
			float num11 = _rotation.w * num2;
			float num12 = _rotation.w * num3;
			mat3 mat = new mat3
			{
				matrix =
				{
					[0, 0] = (float) (1.0 - (num5 + (double) num6)),
					[1, 0] = num7 + num12,
					[2, 0] = num8 - num11,
					[0, 1] = num7 - num12,
					[1, 1] = (float) (1.0 - (num4 + (double) num6)),
					[2, 1] = num9 + num10,
					[0, 2] = num8 + num11,
					[1, 2] = num9 - num10,
					[2, 2] = (float) (1.0 - (num4 + (double) num5))
				}
			};
			return mat;
		}
	}
}