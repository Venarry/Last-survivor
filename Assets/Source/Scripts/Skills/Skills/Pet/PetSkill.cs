using UnityEngine;

public class PetSkill : SkillBehaviour
{
    private readonly PetFactory _petFactory;
    private readonly Transform _owner;
    private Pet _pet;

    public PetSkill(PetFactory petFactory, Transform owner)
    {
        _petFactory = petFactory;
        _owner = owner;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override async void Apply()
    {
        _pet = await _petFactory.Create(_owner.transform.position);
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }
}
