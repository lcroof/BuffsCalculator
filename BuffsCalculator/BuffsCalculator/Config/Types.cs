using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuffsCalculator
{
    public class Types
    {
    }

    public enum BuffAbilityID
    {
        TrickAttack = 1000638,
        Hypercharge = 1001208,
        PiercingResistanceDown = 1000820,
        ChainStratagem = 1001221,
        FoeRequiem = 1000140,
        LeftEye = 1001184,
        TheSpear = 1000832,
        TheArrow = 1000831,
        TheBalance = 1000829,
        BattleVoice = 1000141,
        BattleLitany = 1000786,
        Devotion = 1001213,
        CriticalUp = 1001188,
        MagicVulnerabilityUp = 1000494,
        PhysicalVulnerabilityUp = 1000493,
        Brotherhood = 1001182,
        Embolden = 1001297,
        ExpandedRoyalRoad = 1000817
    }

    public enum DamageAbilityID
    {
        
    }

    public enum DamageHitType
    {
        Normal = 1,
        Critical = 2,
        Direct = 3,
        CriticalAndDirect = 4
    }
}
