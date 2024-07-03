using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Velocity : CharacterStatModifierSO
{
    // Start is called before the first frame update

    public override void AffectCharacter(GameObject character, float val)
    {
        playerMovement player = character.GetComponent<playerMovement>();
        if (player != null)
            player.Velocity(val);
    }
}
