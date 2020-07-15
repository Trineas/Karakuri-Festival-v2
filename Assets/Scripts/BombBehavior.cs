using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject explosionEffect;
    public MeshRenderer whick;
    public Material bomb, bombRed;
    public int explosionSoundToPlay;

    private bool isExploding;

    public void DetonateBomb()
    {
        isExploding = true;
        StartCoroutine(DetonateBombCo());
    }

    public IEnumerator DetonateBombCo()
    {
        Animator anim = this.GetComponent<Animator>();
        MeshRenderer ren = this.GetComponent<MeshRenderer>();

        yield return new WaitForSeconds(2f);

        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        ren.material = bombRed;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1f, 1f, 1f);
        ren.material = bomb;
        yield return new WaitForSeconds(1f);

        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        ren.material = bombRed;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1f, 1f, 1f);
        ren.material = bomb;
        yield return new WaitForSeconds(1f);

        transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        ren.material = bombRed;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1f, 1f, 1f);
        ren.material = bomb;
        yield return new WaitForSeconds(1f);

        transform.localScale = new Vector3(2f, 2f, 2f);
        ren.material = bombRed;
        yield return new WaitForSeconds(0.05f);
        anim.SetTrigger("Explode");
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(explosionSoundToPlay);
        ren.enabled = false;
        whick.enabled = false;

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
        isExploding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(explosionSoundToPlay);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);

            AudioManager.instance.PlaySFX(explosionSoundToPlay);
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }

        else if (other.tag == "Destroyable")
        {
            AudioManager.instance.PlaySFX(explosionSoundToPlay);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        else if (other.tag == "NPC")
        {
            AudioManager.instance.PlaySFX(explosionSoundToPlay);
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        else
        {
            if (!isExploding)
            {
                DetonateBomb();
            }
        }
    }
}
