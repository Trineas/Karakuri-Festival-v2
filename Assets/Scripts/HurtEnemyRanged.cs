using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyRanged : MonoBehaviour
{
    public GameObject destroyEffect;
    public int destroySoundToPlay, hurtSoundToplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (CharacterSwitch.instance.currentCharacter == 1)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else if (CharacterSwitch.instance.currentCharacter == 2)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            AudioManager.instance.PlaySFX(hurtSoundToplay);
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }

        else if (other.tag == "Destroyable")
        {
            if (CharacterSwitch.instance.currentCharacter == 1)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else if (CharacterSwitch.instance.currentCharacter == 2)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }

            else
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        else if (other.tag == "NPC")
        {
            if (CharacterSwitch.instance.currentCharacter == 1)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else if (CharacterSwitch.instance.currentCharacter == 2)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        else if (other.tag == "Environment")
        {
            if (CharacterSwitch.instance.currentCharacter == 1)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else if (CharacterSwitch.instance.currentCharacter == 2)
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else
            {
                AudioManager.instance.PlaySFX(destroySoundToPlay);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
