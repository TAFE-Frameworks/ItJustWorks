using UnityEngine;

using Unity.Mathematics;

namespace ItJustWorks.Threading.Physics
{
	// ReSharper disable once InconsistentNaming
	public struct OBB
	{
		public float3 Position { get; private set; }
		public float3 Size { get; private set; }
		public mat3 Rotation { get; private set; }

		public OBB(BoxCollider _collider)
		{
			Position = _collider.center + _collider.transform.position;
			Size = _collider.size;
			Rotation = mat3.Rotate(_collider.transform.rotation);
		}

		public void Update(BoxCollider _collider)
		{
			Position = _collider.center + _collider.transform.position;
			Size = _collider.size;
			Rotation = mat3.Rotate(_collider.transform.rotation);
		}

		public bool Overlaps(float3 _point, float _radius)
		{
			float3 closest = ClosestPoint(_point);
			float distanceSquared = MagnitudeSquared(_point - closest);
			float radiusSqr = _radius * _radius;

			return distanceSquared < radiusSqr;
		}

		public float3 ClosestPoint(float3 _point)
		{
			float3 result = Position;
			float3 direction = _point - Position;

			for(int i = 0; i < 3; ++i)
			{
				float[] orientation = Rotation[i];
				float3 axis = new float3(orientation[0], orientation[1], orientation[2]);

				float distance = math.dot(direction, axis);

				if(distance > Size[i])
					distance = Size[i];

				if(distance < -Size[i])
					distance = -Size[i];

				result += axis * distance;
			}

			return result;
		}

		private float MagnitudeSquared(Vector3 _point) => _point.magnitude * _point.magnitude;
	}
}