/* Created by and for usage of FF Studios (2022). */

namespace FFStudio
{
    public interface IClusterEntity
	{
		void Subscribe_Cluster();
		void UnSubscribe_Cluster();
		void OnUpdate_Cluster();
		int GetID();
	}

	public interface IJSONEntity
	{
		string ConvertToJSON();
		void OverriteFromJSON( string json );
	}

	public interface IInteractable
	{
		void OnInteract();
	}
}