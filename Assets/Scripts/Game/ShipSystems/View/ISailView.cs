using UnityEngine;

namespace ShipSystems
{
    public interface ISailView
    {
        float Progress { set; }
        Vector3 Wind { set; }
        
    }
}