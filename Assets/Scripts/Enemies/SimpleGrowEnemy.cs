using UnityEngine;

public class SimpleGrowEnemy : BaseEnemy
{
    public Animator Animator;
    public Renderer Renderer;
    public Transform GrowTransform;
    
    protected override Material GetMaterial()
    {
        return Renderer.material;
    }

    protected override float GetGrowSize()
    {
        return GrowTransform.localScale.y;
    }

    public override void Grow(bool isError = false)
    {
        // Calculate grow amount and set the scale to it
        var growAmount = _growSpeed * Time.deltaTime;
        if (isError)
        {
            growAmount *= _errorMultiplier;
        }
        GrowTransform.localScale += Vector3.one * growAmount;
    }

    public override void Die()
    {
        Animator.SetTrigger("Die");
    }

    protected override void BlowUp()
    {
        Animator.SetTrigger("BlowUp");
    }

    // This function is called from the enemy's die animation
    public void OnDieEvent()
    {
        // Call BaseEnemy.Die() to trigger the event
        base.Die();
        
        // Destroy this object
        Destroy(gameObject);
    }

    // This function is called from the enemy's blow up animation
    public void OnBlowUpEvent()
    {
        // Call BaseEnemy.BlowUp() to trigger the event
        base.BlowUp();
        
        // Destroy this object
        Destroy(gameObject);
    }
}