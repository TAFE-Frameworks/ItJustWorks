using UnityEngine;

namespace ItJustWorks
{
	public static class Vector3Extensions
	{
		public static float[] ToArray(this Vector3 _vector3)
		{
			return new[] {_vector3.x, _vector3.y, _vector3.z};
		}
	}
}