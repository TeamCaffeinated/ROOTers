// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
	using Coherence.ProtocolDef;
	using Coherence.Serializer;
	using Coherence.SimulationFrame;
	using Coherence.Entity;
	using Coherence.Utils;
	using Coherence.Brook;
	using Coherence.Toolkit;
	using UnityEngine;

	public struct GenericScale : ICoherenceComponentData
	{
		public Vector3 value;

		public override string ToString()
		{
			return $"GenericScale(value: {value})";
		}

		public uint GetComponentType() => Definition.InternalGenericScale;

		public const int order = 0;

		public int GetComponentOrder() => order;

		public AbsoluteSimulationFrame Frame;
	

		public void SetSimulationFrame(AbsoluteSimulationFrame frame)
		{
			Frame = frame;
		}

		public AbsoluteSimulationFrame GetSimulationFrame() => Frame;

		public ICoherenceComponentData MergeWith(ICoherenceComponentData data, uint mask)
		{
			var other = (GenericScale)data;
			if ((mask & 0x01) != 0)
			{
				Frame = other.Frame;
				value = other.value;
			}
			mask >>= 1;
			return this;
		}

		public static void Serialize(GenericScale data, uint mask, IOutProtocolBitStream bitStream)
		{
			if (bitStream.WriteMask((mask & 0x01) != 0))
			{
				bitStream.WriteVector3((data.value.ToCoreVector3()), FloatMeta.NoCompression());
			}
			mask >>= 1;
		}

		public static (GenericScale, uint, uint?) Deserialize(InProtocolBitStream bitStream)
		{
			var mask = (uint)0;
			var val = new GenericScale();
	
			if (bitStream.ReadMask())
			{
				val.value = (bitStream.ReadVector3(FloatMeta.NoCompression())).ToUnityVector3();
				mask |= 0b00000000000000000000000000000001;
			}
			return (val, mask, null);
		}
		public static (GenericScale, uint, uint?) DeserializeArchetypeContainer_11cb97191c21e4e609b0324a7f00b18d_GenericScale_LOD0(InProtocolBitStream bitStream)
		{
			var mask = (uint)0;
			var val = new GenericScale();
			if (bitStream.ReadMask())
			{
				val.value = (bitStream.ReadVector3(FloatMeta.NoCompression())).ToUnityVector3();
				mask |= 0b00000000000000000000000000000001;
			}

			return (val, mask, 0);
		}

		/// <summary>
		/// Resets byte array references to the local array instance that is kept in the lastSentData.
		/// If the array content has changed but remains of same length, the new content is copied into the local array instance.
		/// If the array length has changed, the array is cloned and overwrites the local instance.
		/// If the array has not changed, the reference is reset to the local array instance.
		/// Otherwise, changes to other fields on the component might cause the local array instance reference to become permanently lost.
		/// </summary>
		public void ResetByteArrays(ICoherenceComponentData lastSent, uint mask)
		{
			var last = lastSent as GenericScale?;
	
		}
	}
}