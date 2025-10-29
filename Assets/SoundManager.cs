using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Khai báo các AudioClip cho các sound đã liệt kê
    public AudioClip bombExplode;
    public AudioClip bombFuse;
    public AudioClip throwBomb;
    public AudioClip throwFruit;
    public AudioClip bonusBananaFreeze;
    public AudioClip bonusBananaDouble;
    public AudioClip bonusBananaFrenzy;
    public AudioClip powerupDeflect;
    public AudioClip[] comboSounds; // combo1, combo2,...combo8
    public AudioClip bonusCountUp;
    public AudioClip gameOver;
    public AudioClip gameStart;
    public AudioClip gank;
    public AudioClip pomeBurst;
    public AudioClip pomeRampdow;
    public AudioClip pomeLp;
    public AudioClip[] pomeSlice; // pomeSlice1,2,3
    public AudioClip pomeZoomout;
    public AudioClip swordSwipe1;
    public AudioClip swordSwipe2;
    public AudioClip swordSwipe3;
    public AudioClip swordSwipe4;
    public AudioClip swordSwipe5;
    public AudioClip swordSwipe6;
    public AudioClip swordSwipe7;
    public AudioClip timeTick;
    public AudioClip timeTock;
    public AudioClip timeUp;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    // Các hàm phát âm thanh đơn giản, có thể bổ sung overload nếu cần
    public void PlayBombExplode() => audioSource.PlayOneShot(bombExplode);
    public void PlayBombFuse() => audioSource.PlayOneShot(bombFuse);
    public void PlayThrowBomb() => audioSource.PlayOneShot(throwBomb);
    public void PlayThrowFruit() => audioSource.PlayOneShot(throwFruit);
    public void PlayBonusBananaFreeze() => audioSource.PlayOneShot(bonusBananaFreeze);
    public void PlayBonusBananaDouble() => audioSource.PlayOneShot(bonusBananaDouble);
    public void PlayBonusBananaFrenzy() => audioSource.PlayOneShot(bonusBananaFrenzy);
    public void PlayPowerupDeflect() => audioSource.PlayOneShot(powerupDeflect);
    public void PlayCombo(int combo)
    {
        // combo từ 1-8
        if (combo >= 1 && combo <= comboSounds.Length)
            audioSource.PlayOneShot(comboSounds[combo - 1]);
    }
    public void PlayBonusCountUp() => audioSource.PlayOneShot(bonusCountUp);
    public void PlayGameOver() => audioSource.PlayOneShot(gameOver);
    public void PlayGameStart() => audioSource.PlayOneShot(gameStart);
    public void PlayGank() => audioSource.PlayOneShot(gank);
    public void PlayPomeBurst() => audioSource.PlayOneShot(pomeBurst);
    public void PlayPomeRampdow() => audioSource.PlayOneShot(pomeRampdow);
    public void PlayPomeLp() => audioSource.PlayOneShot(pomeLp);
    public void PlayPomeSlice(int index)
    {
        // slice từ 1-3
        if (index >= 1 && index <= pomeSlice.Length)
            audioSource.PlayOneShot(pomeSlice[index - 1]);
    }
    public void PlayPomeZoomout() => audioSource.PlayOneShot(pomeZoomout);

    public void PlaySwordSwipeRandom()
    {
        int idx = Random.Range(1, 8); // swipe1-7
        AudioClip clip = null;
        switch (idx)
        {
            case 1: clip = swordSwipe1; break;
            case 2: clip = swordSwipe2; break;
            case 3: clip = swordSwipe3; break;
            case 4: clip = swordSwipe4; break;
            case 5: clip = swordSwipe5; break;
            case 6: clip = swordSwipe6; break;
            case 7: clip = swordSwipe7; break;
        }
        if (clip != null) audioSource.PlayOneShot(clip);
    }

    public void PlayTimeTick() => audioSource.PlayOneShot(timeTick);
    public void PlayTimeTock() => audioSource.PlayOneShot(timeTock);
    public void PlayTimeUp() => audioSource.PlayOneShot(timeUp);
}