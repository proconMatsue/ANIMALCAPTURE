using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//必須コンポーネントをアタッチしておく
[RequireComponent(typeof(SurfaceMeshesToPlanes))]
[RequireComponent(typeof(RemoveSurfaceVertices))]

public class MyPlaySpaceManager : Singleton<MyPlaySpaceManager>
{
    [SerializeField]
    [Tooltip("壁を作成するまでの時間")]
    private float ScanTime = 30.0f;

    [SerializeField]
    [Tooltip("スキャン中にメッシュに貼るマテリアル")]
    private Material ScaningMaterial;


    //メッシュ化が終了したかどうかを示すフラグ
    private bool meshesProcessed = false;

    /// <summary>
    /// このスクリプト開始時に一同だけ呼び出される
    /// </summary>
    private void Start()
    {
        //いちいち設定アタッチするのがめんどくさいため, デフォルトでwireframeにしておく
        ScaningMaterial = new Material(Shader.Find("wireframe"));

        //メッシュ化は終了していないため, 偽にしておく
        meshesProcessed = false;

        //スキャン中のマテリアルを設定
        SpatialMappingManager.Instance.SetSurfaceMaterial(ScaningMaterial);

        //メッシュ化が終了した際に
        //SurfaceMeshesToPlanes_MakePlanesComplete関数
        //を呼び出すようにしている
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    /// <summary>
    /// 一定の時間が経過したらメッシュ部分にplaneオブジェクトを当てはめるようにする
    /// </summary>
    private void Update()
    {
        //メッシュ化されているかどうかを判断
        if (!meshesProcessed)
        {
            //指定したスキャン(メッシュ化)時間を経過したかどうかを判断
            if ((Time.time - SpatialMappingManager.Instance.StartTime) < ScanTime)
            {
                //経過していなかったら, 何もせずに終了
            }
            else
            {
                //指定した時間が経過していたら...

                //メッシュを作成するためのオブザーバーが走っていたら...
                if (SpatialMappingManager.Instance.IsObserverRunning())
                {
                    //メッシュの作成を終了
                    SpatialMappingManager.Instance.StopObserver();
                }

                //メッシュ部分にplaneオブジェクトを張り替えていく
                CreatePlane();

                //メッシュ化が終了したフラグを真にする
                meshesProcessed = true;
            }
        }
    }

    /// <summary>
    /// 壁が作成された後に呼ばれる関数
    /// </summary>
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        //平面内の頂点を削除
        RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

        //メッシュを消す
        SpatialMappingManager.Instance.DrawVisualMeshes = false;
    }

    /// <summary>
    /// メッシュ部分にplaneオブジェクトを張り替えていく関数
    /// </summary>
    private void CreatePlane()
    {
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            //メッシュ部分に壁を貼っていく
            surfaceToPlanes.MakePlanes();
        }
    }

    /// <summary>
    /// 平面に内包された頂点を削除する関数
    /// </summary>
    /// <param name="boundingObjects"></param>
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            //頂点の削除
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }
}
