// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Coherence.Toolkit;
	using Coherence.Toolkit.Bindings;
	using Coherence.Entity;
	using Coherence.ProtocolDef;
	using Coherence.Brook;
	using Coherence.Toolkit.Bindings.ValueBindings;
	using Coherence.Toolkit.Bindings.TransformBindings;
	using Coherence.Connection;
	using Coherence.Log;
	using Logger = Coherence.Log.Logger;
	using UnityEngine.Scripting;

	public class Binding_11cb97191c21e4e609b0324a7f00b18d_610fcdca_8555_485a_9eba_61f8dad84ada : Vector3Binding
	{
		private DummyRotate CastedUnityComponent;		

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (DummyRotate)UnityComponent;
		}
		public override string CoherenceComponentName => "Container_DummyRotate__4070835482919991762";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get => (Vector3)(UnityEngine.Vector3)(CastedUnityComponent.myRotation);
			set => CastedUnityComponent.myRotation = (UnityEngine.Vector3)(value);
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_DummyRotate__4070835482919991762)coherenceComponent;
			return update.myRotation;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_DummyRotate__4070835482919991762)coherenceComponent;
			update.myRotation = Value;
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Container_DummyRotate__4070835482919991762();
		}
	}

	public class Binding_11cb97191c21e4e609b0324a7f00b18d_bedeabb3_6a03_47fe_a99e_3d2c34cc3f4b : ColorBinding
	{
		private RandomColourSetter CastedUnityComponent;		

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (RandomColourSetter)UnityComponent;
		}
		public override string CoherenceComponentName => "Container_RandomColourSetter_8707848426183972558";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Color Value
		{
			get => (Color)(UnityEngine.Color)(CastedUnityComponent.cubeColor);
			set => CastedUnityComponent.cubeColor = (UnityEngine.Color)(value);
		}

		protected override Color ReadComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_RandomColourSetter_8707848426183972558)coherenceComponent;
			return update.cubeColor;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_RandomColourSetter_8707848426183972558)coherenceComponent;
			update.cubeColor = Value;
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Container_RandomColourSetter_8707848426183972558();
		}
	}

	public class Binding_11cb97191c21e4e609b0324a7f00b18d_481f71b0_3968_4b9d_9e71_6a59587b50aa : BoolBinding
	{
		private RandomColourSetter CastedUnityComponent;		

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (RandomColourSetter)UnityComponent;
		}
		public override string CoherenceComponentName => "Container_RandomColourSetter_8707848426183972558";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override bool Value
		{
			get => (bool)(System.Boolean)(CastedUnityComponent.colorSet);
			set => CastedUnityComponent.colorSet = (System.Boolean)(value);
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_RandomColourSetter_8707848426183972558)coherenceComponent;
			return update.colorSet;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent)
		{
			var update = (Container_RandomColourSetter_8707848426183972558)coherenceComponent;
			update.colorSet = Value;
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Container_RandomColourSetter_8707848426183972558();
		}
	}


	[Preserve]
	[AddComponentMenu("coherence/Baked/Baked 'Container' (auto assigned)")]
	[RequireComponent(typeof(CoherenceSync))]
	public class CoherenceSyncContainer : CoherenceSyncBaked
	{
		private CoherenceSync coherenceSync;
		private Logger logger;

		// Cached targets for commands

		private IClient client;
		private CoherenceMonoBridge monoBridge => coherenceSync.MonoBridge;

		protected void Awake()
		{
			coherenceSync = GetComponent<CoherenceSync>();
			coherenceSync.usingReflection = false;

			logger = coherenceSync.logger.With<CoherenceSyncContainer>();
			if (coherenceSync.TryGetBindingByGuid("610fcdca-8555-485a-9eba-61f8dad84ada", "myRotation", out Binding InternalContainer_DummyRotate__4070835482919991762_Container_DummyRotate__4070835482919991762_myRotation))
			{
				var clone = new Binding_11cb97191c21e4e609b0324a7f00b18d_610fcdca_8555_485a_9eba_61f8dad84ada();
				InternalContainer_DummyRotate__4070835482919991762_Container_DummyRotate__4070835482919991762_myRotation.CloneTo(clone);
				coherenceSync.Bindings[coherenceSync.Bindings.IndexOf(InternalContainer_DummyRotate__4070835482919991762_Container_DummyRotate__4070835482919991762_myRotation)] = clone;
			}
			else
			{
				logger.Error("Couldn't find binding (DummyRotate).myRotation");
			}
			if (coherenceSync.TryGetBindingByGuid("bedeabb3-6a03-47fe-a99e-3d2c34cc3f4b", "cubeColor", out Binding InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_cubeColor))
			{
				var clone = new Binding_11cb97191c21e4e609b0324a7f00b18d_bedeabb3_6a03_47fe_a99e_3d2c34cc3f4b();
				InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_cubeColor.CloneTo(clone);
				coherenceSync.Bindings[coherenceSync.Bindings.IndexOf(InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_cubeColor)] = clone;
			}
			else
			{
				logger.Error("Couldn't find binding (RandomColourSetter).cubeColor");
			}
			if (coherenceSync.TryGetBindingByGuid("481f71b0-3968-4b9d-9e71-6a59587b50aa", "colorSet", out Binding InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_colorSet))
			{
				var clone = new Binding_11cb97191c21e4e609b0324a7f00b18d_481f71b0_3968_4b9d_9e71_6a59587b50aa();
				InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_colorSet.CloneTo(clone);
				coherenceSync.Bindings[coherenceSync.Bindings.IndexOf(InternalContainer_RandomColourSetter_8707848426183972558_Container_RandomColourSetter_8707848426183972558_colorSet)] = clone;
			}
			else
			{
				logger.Error("Couldn't find binding (RandomColourSetter).colorSet");
			}
		}

		public override List<ICoherenceComponentData> CreateEntity()
		{
			if (coherenceSync.UsesLODsAtRuntime && (Archetypes.IndexForName.TryGetValue(coherenceSync.Archetype.ArchetypeName, out int archetypeIndex)))
			{
				var components = new List<ICoherenceComponentData>()
				{
					new ArchetypeComponent
					{
						index = archetypeIndex
					}
				};

				return components;
			}
			else
			{
				logger.Warning($"Unable to find archetype {coherenceSync.Archetype.ArchetypeName} in dictionary. Please, bake manually (coherence > Bake)");
			}

			return null;
		}

		public override void Initialize(CoherenceSync sync, IClient client)
		{
			if (coherenceSync == null)
			{
				coherenceSync = sync;
			}
			this.client = client;
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				default:
					logger.Warning($"[CoherenceSyncContainer] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
