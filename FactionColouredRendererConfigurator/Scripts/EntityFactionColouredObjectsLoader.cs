using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTSEngine.EntityComponent;
using RTSEngine.Entities;
using RTSEngine.Utilities;
using RTSEngine.Event;
using RTSEngine.Game;
using UnityEngine.Rendering;

public class EntityColouredObjectsLoader : MonoBehaviour, IEntityPostInitializable
{
    [HideInInspector]
    public int TabID = 0;

    public bool IsRendererLoaded = false;

    public bool UseParentNotList = true;
    public Transform ObjectsParent = null;

    public List<MeshRenderer> ObjectsToColour = new();

    private IEntity entity;

    [Space(), Tooltip("What parts of the model will be colored with the faction colors?")]
    public List<ColoredRenderer> coloredRenderers = new();
    public List<string> MaterialPropertyNames = new();

    public void OnEntityPostInit(IGameManager gameMgr, IEntity entity)
    {
        this.entity = entity;

        this.entity.FactionUpdateComplete += HandleFactionUpdateComplete;

        SetColors();
    }

    public void SetRenderers(List<MeshRenderer> renderers)
    {
        if (renderers.Count > 0)
        {
            if(MaterialPropertyNames.Count == 0)
            {
                MaterialPropertyNames.Add("_Color");
            }
            foreach (MeshRenderer renderer in renderers)
            {
                if(MaterialPropertyNames.Count > 0)
                {
                    foreach(string name in MaterialPropertyNames)
                    {
                        if(name != "")
                        {
                            ColoredRenderer toAdd = new();
                            toAdd.renderer = renderer;
                            toAdd.colorPropertyName = name;
                            coloredRenderers.Add(toAdd);
                        }
                    }
                }
            }
        }
    }

    public void Disable()
    {

    }
    private void HandleFactionUpdateComplete(IEntity sender, FactionUpdateArgs args)
    {
        SetColors();
    }

    private void SetColors()
    {
        foreach (ColoredRenderer cr in coloredRenderers)
            cr.UpdateColor(entity.IsFree ? Color.white : entity.Slot.Data.color, entity);
    }

}
