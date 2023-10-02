//using UnityEditor;
//using UnityEngine;
//public class EditorNormalDrawer
//{
//    [DrawGizmo(GizmoType.Selected)]
//    private static void DrawGizmo(MeshFilter meshFilter, GizmoType type)
//    {
//        Draw(meshFilter.sharedMesh, meshFilter.transform);
//    }
//    [DrawGizmo(GizmoType.Selected)]
//    private static void DrawGizmo(SkinnedMeshRenderer meshRenderer, GizmoType type)
//    {
//        Draw(meshRenderer.sharedMesh, meshRenderer.transform);
//    }
//    private static void Draw(Mesh mesh, Transform transform)
//    {
//        if (mesh.normals.Length != mesh.vertices.Length)
//            return;
//        var rotation = transform.rotation;
//        var position = transform.position;
//        var scale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
//        Gizmos.color = Color.red;
//        for (var i = 0; i < mesh.vertices.Length; i++)
//        {
//            var rotatePos = rotation * mesh.vertices[i];
//            for (var j = 0; j < 3; j++)
//                rotatePos[j] *= transform.localScale[j];
//            Gizmos.DrawLine(position + rotatePos, position + rotatePos + mesh.normals[i] * 0.1f);
//        }
//    }
//}
