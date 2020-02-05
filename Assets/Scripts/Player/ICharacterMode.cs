using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMode
{
    void UpdateActions();
    void ToMovementMode();
    void ToIdleMode();
    void ToJumpMode();
    void ToAttackMode();
    void ToDeadMode();
    void ToRotationMode();
    void ToTrampolineMode();
    void ToVerticalTubeMode();
}
