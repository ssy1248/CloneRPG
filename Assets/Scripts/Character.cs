using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic
{
    Life,
    Energy,
    Damage,
    Armor,
    AttackSpeed,
    MoveSpeed,
    HealthRegeneration
}

[Serializable]
public class StatsValue
{
    public Statistic statisticType;
    public bool typeFloat;
    public int interget_value;

    public float float_value;

    public StatsValue(Statistic statisticType, int value = 0)
    {
        this.statisticType = statisticType;
        this.interget_value = value;
    }

    public StatsValue(Statistic statisticType, float float_value)
    {
        this.statisticType = statisticType;
        this.float_value = float_value;
        typeFloat = true;
    }
}

[Serializable]
public class StatsGroup
{
    public List<StatsValue> stats;

    public StatsGroup()
    {
        stats = new List<StatsValue>();
    }

    public void Init()
    {
        stats.Add(new StatsValue(Statistic.Life, 100));
        stats.Add(new StatsValue(Statistic.Energy, 100));
        stats.Add(new StatsValue(Statistic.Damage, 25));
        stats.Add(new StatsValue(Statistic.Armor, 5));
        stats.Add(new StatsValue(Statistic.AttackSpeed, 1f));
        stats.Add(new StatsValue(Statistic.MoveSpeed, 1f));
        stats.Add(new StatsValue(Statistic.HealthRegeneration, 1f));
    }

    internal StatsValue Get(Statistic statisticToGet)
    {
        return stats[(int)statisticToGet];
    }

    internal void Sum(StatsValue toAdd)
    {
        StatsValue statsValue = stats[(int)toAdd.statisticType];

        if(toAdd.typeFloat == true)
        {
            statsValue.float_value += toAdd.float_value;
        }
        else
        {
            statsValue.interget_value += toAdd.interget_value;
        }
    }

    public void Subtract(StatsValue toSubtract)
    {
        StatsValue statsValue = stats[(int)toSubtract.statisticType];

        if(toSubtract.typeFloat == true)
        {
            statsValue.float_value -= toSubtract.float_value;
        }
        else
        {
            statsValue.interget_value -= toSubtract.interget_value;
        }
    }
}

public enum Attribute
{
    Strength,
    Dexterity,
    Intelligence
}

[Serializable]
public class AttributeValue
{
    public Attribute attributeType;
    public int value;

    public AttributeValue(Attribute attributeType, int value = 0)
    {
        this.attributeType = attributeType;
        this.value = value;
    }
}

[Serializable]
public class AttributeGroup
{
    public List<AttributeValue> attributeValues;

    public AttributeGroup()
    {
        attributeValues = new List<AttributeValue>();
    }

    public void Init()
    {
        attributeValues.Add(new AttributeValue(Attribute.Strength));
        attributeValues.Add(new AttributeValue(Attribute.Dexterity));
        attributeValues.Add(new AttributeValue(Attribute.Intelligence));
    }

    public AttributeValue Get(Attribute attributeToShow)
    {
        return attributeValues[(int)attributeToShow];
    }
}

[Serializable]
public class ValuePool
{
    public StatsValue maxValue;
    public int currentValue;

    public ValuePool(StatsValue maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue.interget_value;
    }

    internal void FullRestore()
    {
        currentValue = maxValue.interget_value;
    }

    public void Restore(int v)
    {
        currentValue += v;

        if(currentValue > maxValue.interget_value)
        {
            currentValue = maxValue.interget_value;
        }
    }
}

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField]
    AttributeGroup attributes;
    [SerializeField]
    StatsGroup stats;

    public ValuePool lifePool;
    public ValuePool energyPool;
    public bool isDead;

    private void Start()
    {
        //능력치 부분 생성
        attributes = new AttributeGroup();
        attributes.Init();

        //플레이어 스탯 생성
        stats = new StatsGroup(); 
        stats.Init();

        lifePool = new ValuePool(stats.Get(Statistic.Life));
        energyPool = new ValuePool(stats.Get(Statistic.Energy));
    }

    private void Update()
    {
        LifeRegeneration();
    }

    float lifeRegen;

    private void LifeRegeneration()
    {
        lifeRegen += Time.deltaTime * stats.Get(Statistic.HealthRegeneration).float_value;

        if(lifeRegen > 1f)
        {
            Heal(1);
            lifeRegen -= 1f;
        }
    }

    private void Heal(int v)
    {
        lifePool.Restore(v);
    }

    public void TakeDamage(int damage)
    {
        damage = ApplyDefence(damage);

        lifePool.currentValue -= damage;

        CheckDeath();
    }

    private int ApplyDefence(int damage)
    {
        damage -= stats.Get(Statistic.Armor).interget_value;

        //방어력이 공격력보다 높을때
        if(damage <= 0)
        {
            damage = 1;
        }

        return damage;
    }

    private void CheckDeath()
    {
        if(lifePool.currentValue <= 0)
        {
            isDead = true;
            GetComponent<CharacterDefeatHandler>().Deafeated();
        }
    }

    public StatsValue GetStatsValue(Statistic statisticToGet)
    {
        return stats.Get(statisticToGet);
    }

    public void Restore()
    {
        lifePool.FullRestore();
        isDead = false;
    }

    internal void AddStats(List<StatsValue> statsValue)
    {
        for(int i = 0; i < statsValue.Count; i++)
        {
            StatsAdd(statsValue[i]);
        }
    }

    private void StatsAdd(StatsValue statsValue)
    {
        stats.Sum(statsValue);
    }

    public void SubtractStats(List<StatsValue> statsValues)
    {
        for(int i = 0; i < statsValues.Count; i++)
        {
            SubtractStats(statsValues[i]);
        }
    }

    private void SubtractStats(StatsValue statsValue)
    {
        stats.Subtract(statsValue);
    }

    public int GetDamage()
    {
        int damage = GetStatsValue(Statistic.Damage).interget_value;
        return damage;
    }

    public ValuePool GetLifePool()
    {
        return lifePool;
    }

    public AttributeValue GetAttributeValue(Attribute attributeToShow)
    {
        return attributes.Get(attributeToShow);
    }
}
