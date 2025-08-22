using UnityEngine;

namespace RobotBattle.Robot
{
    public class MaterialChangerScript : MonoBehaviour
    {
        private MeshRenderer[] meshRenderers;
        private SkinnedMeshRenderer[] skinnedMeshRenderers;

        public void SetMaterial(MaterialType materialType)
        {
            var material = Resources.Load<Material>("Materials/" + materialType);

            meshRenderers ??= GetComponentsInChildren<MeshRenderer>();

            foreach (var item in meshRenderers)
            {
                if (!item.CompareTag("Shield") && item.name != "Cube") item.material = material;
            }

            skinnedMeshRenderers ??= GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var item in skinnedMeshRenderers)
            {
                if (!item.CompareTag("Shield")) item.material = material;
            }
        }
    }
}