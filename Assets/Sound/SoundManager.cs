using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    /// <summary>マスターボリューム</summary>
    static float m_masterVolume = 1.0f;
    /// <summary>BGMボリューム</summary>
    static float m_bgmVolume = 1.0f;
    /// <summary>SEボリューム</summary>
    static float m_seVolume = 1.0f;
    /// <summary>PhotonView</summary>
    PhotonView m_view;

    /// <summary>BGM用のAudioSource</summary>
    [Header("BGM/SEのAudioSourceの設定")]
    [SerializeField] AudioSource m_bgmAudioSource;
    /// <summary>SE用のAudioSource</summary>
    [SerializeField] AudioSource m_seAudioSource;

    /// <summary>マスター用スライダー</summary>
    [Header("スライダーの設定")]
    [SerializeField] Slider m_masterSlider;
    /// <summary>BGM用スライダー</summary>
    [SerializeField] Slider m_bgmSlider;
    /// <summary>SE用スライダー</summary>
    [SerializeField] Slider m_seSlider;

    /// <summary>BGMのデータ</summary>
    [Header("BGM/SEのデータ")]
    [SerializeField] AudioClip[] m_bgmData;
    /// <summary>SEのデータ</summary>
    [SerializeField] AudioClip[] m_seData;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        m_view = GetComponent<PhotonView>();
        m_bgmAudioSource = m_bgmAudioSource.GetComponent<AudioSource>();
        m_bgmAudioSource = m_bgmAudioSource.GetComponent<AudioSource>();
    }

    void Update()
    {
        m_bgmAudioSource.volume = m_bgmVolume * m_masterVolume;
        m_seAudioSource.volume = m_seVolume * m_masterVolume;
    }

    /// <summary>
    /// 設定されたBGMを再生する
    /// </summary>
    /// <param name="index"></param>
    [PunRPC]
    public void PlayBGM(int index)
    {
        m_bgmAudioSource.clip = m_bgmData[index];
        m_bgmAudioSource.Play();
    }

    /// <summary>
    /// 設定されたSEを再生する
    /// </summary>
    /// <param name="index">seのインデックス</param>
    [PunRPC]
    public void PlaySE(int index)
    {
        m_seAudioSource.PlayOneShot(m_seData[index]);
    }

    /// <summary>
    /// マスターのボリュームをセット/スライダーのOnValueChangedに設定
    /// </summary>
    public void SetMasterVolume()
    {
        m_masterVolume = m_masterSlider.GetComponent<Slider>().value;
    }

    /// <summary>
    /// BGMのボリュームをセット/スライダーのOnValueChangedに設定
    /// </summary>
    public void SetBgmVolume()
    {
        m_bgmVolume = m_bgmSlider.GetComponent<Slider>().value;
    }

    /// <summary>
    /// SEのボリュームをセット/スライダーのOnValueChangedに設定
    /// </summary>
    public void SetSeVolume()
    {
        m_seVolume = m_seSlider.GetComponent<Slider>().value;
    }

    /// <summary>
    /// RPCテスト用
    /// </summary>
    public void RPCTest()
    {
        m_view.RPC("PlaySE", RpcTarget.All, 0);
    }
}
