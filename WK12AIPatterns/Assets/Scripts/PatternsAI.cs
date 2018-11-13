using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsAI : MonoBehaviour
{

    public struct PatternStep
    {
        public Vector3 direction;
        public float stepDuration;
        public string action;

        public PatternStep(Vector2 d, float time)
        {
            direction = d;
            stepDuration = time;
            action = "";
        }

        public PatternStep(Vector2 d, float time, string a)
        {
            direction = d;
            stepDuration = time;
            action = a;
        }
    }

    public PatternStep[] patternSteps;
    private float patternStepTimer = 0;
    private int patternStepIndex = 0;
    public Vector2 dir;

    private EnemyAI enemyAi;

    bool waitingForStepToComplete = false;

    void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
        dir = Vector2.zero;
        patternSteps = new PatternStep[4];
        patternSteps[0] = new PatternStep(Vector2.down, 3);
        patternSteps[1] = new PatternStep(Vector2.left, 4);
        patternSteps[2] = new PatternStep(Vector2.up, 3);
        patternSteps[3] = new PatternStep(Vector2.right, 4);
        //patternSteps[4] = new PatternStep(Vector2.zero, 3, "attack");
    }

    public Vector3 evaluatePattern()
    {
        patternStepTimer += Time.deltaTime;
  
        dir = patternSteps[patternStepIndex].direction;
        if (patternSteps[patternStepIndex].action == "attack")
        {
            dir = enemyAi.deltaTrack();
            enemyAi.LightOn();
        }
        else
            enemyAi.LightOff();
        if (!waitingForStepToComplete)
        {
            waitingForStepToComplete = true;
            StartCoroutine(nextPatternStep(patternSteps[patternStepIndex]));
        }
        //if (patternStepTimer > patternSteps[patternStepIndex].stepDuration)
        //{
        //    patternStepTimer = 0;
        //    patternStepIndex++;
        //    patternStepIndex %= patternSteps.Length;
        //}
        dir.Normalize();
        return dir;
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator nextPatternStep(PatternStep s){
        yield return new WaitForSeconds(s.stepDuration);
        ++patternStepIndex;
        patternStepIndex %= patternSteps.Length;
        waitingForStepToComplete = false;
    }
}

