using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string GunName;  // 총의 이름
    public float Range;     // 총의 사정 거리
    public float FireRate;  // 연사 속도 (한발과 한발간의 시간 텀)
    public float ReloadTime;// 재장전 속도
    public int Damage;      // 총의 공격력
    public float RetroActionForce;  // 반동 세기

    public int ReloadBulletCount;   // 총의 재장전 개수
    public int CurrentBulletCount;  // 현재 탄창에 남아있는 총알의 개수
    public int MaxBulletCount;      // 총알 최대 소유 개수
    public int CarryBulletCount;    // 현재 소유하고 있는 총알의 총 개수

    public Animator Anim; 
    public ParticleSystem MuzzleFlash;  // 화염구 이펙트
    public GameObject HitEffectPrefab;  // 총알 피격 이펙트
    public AudioClip Fire_Sound;    // 총 발사 소리 오디오 클립

}
