using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    public Level currentLevel;

    public Player player;
    private List<Enemy> enemies = new List<Enemy>();

    private bool isRevive;

    private int totalEnemy;

    private int levelIndex;

    public int TotalCharacter => totalEnemy + enemies.Count + 1;

    private void Start()
    {
        player.OnInit();
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
    }
    public void OnInit()
    {
        isRevive = false;
        for(int i = 0; i < currentLevel.botInTable; i++)
        {
            NewEnemy(null);
        }

        totalEnemy = currentLevel.botTotal - currentLevel.botInTable - 1;
    }

    public void OnReset()
    {
        player.OnDespawn();
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnDespawn();
        }
        enemies.Clear();
        SimplePool.CollectAll();
    }

    public Vector3 RandomPoint()
    {
        Vector3 randPoint = Vector3.zero;

        float size = Character.ATTACK_RANGE + Character.MAX_SIZE + 1f;

        for(int e = 0; e < 50; e++)
        {
            randPoint = currentLevel.RandomPoint();
            if(Vector3.Distance(randPoint, player.transform.position) < size)
            {
                continue;
            }
            for(int j = 0; j < 20; j++)
            {
                for(int i = 0; i < enemies.Count; i++)
                {
                    if (Vector3.Distance(randPoint, enemies[i].transform.position) < size)
                    {
                        break;
                    }
                }

                if (j == 19)
                {
                    return randPoint;
                }
            }
        }
        return randPoint;
    }

    public void CharacterDeath(Character c)
    {
        if(c is Player)
        {
            UIManager.Ins.CloseAll();
            if (!isRevive)
            {
                isRevive = true;
                UIManager.Ins.OpenUI<UIRevive>();

            }
            else
            {
                Fail();
            }
            
        }
        else if(c is Enemy)
        {
            enemies.Remove(c as Enemy);
            if(GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting))
            {
                NewEnemy(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
            }
            else
            {
                if (totalEnemy > 0)
                {
                    totalEnemy--;
                    NewEnemy(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
                }
                if(enemies.Count == 0)
                {
                    Victory();
                }
            }
        }
        UIManager.Ins.GetUI<UIGameplay>().UpdateTotalCharacter();
    }

    public void NewEnemy(IState<Enemy> state)
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, RandomPoint(), Quaternion.identity);
        enemy.OnInit();
        enemy.ChangeState(state);
        enemies.Add(enemy);

    }

    public void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>().SetCoin(player.Coin);
        player.ChangeAnim(Constant.ANIM_DANCE);
        
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIFail>().SetCoin(player.Coin);
    }

    public void Home()
    {
        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void OnPlay()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ChangeState(new PatrolState());
        }
    }

    public void OnRevive()
    {
        player.TF.position = RandomPoint() + new Vector3(0, 1f, 0);
        player.OnRevive();
    }
    public void OnLoadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[level]);
    }

    public void NextLevel()
    {
        levelIndex++;
    }

    public void SetTargetIndicatorAlpha(float alpha)
    {
        /*List<GameUnit> list = SimplePool.GetAllUnitIsActive(PoolType.TargetIndicator);

        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as TargetIndicator).SetAlpha(alpha);
        }*/
    }
}
