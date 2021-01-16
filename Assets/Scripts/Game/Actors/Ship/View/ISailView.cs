using UnityEngine;

namespace Game.Actors.Ship.View
{
    public interface ISailView
    {
        float Progress { set; }
        Vector3 Wind { set; }
        
    }
}