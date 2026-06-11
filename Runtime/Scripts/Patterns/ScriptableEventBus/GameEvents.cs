using UnityEngine;

[CreateAssetMenu(menuName = "EngineData/EventBus/Int Event")]
public class IntGameEvent : BaseGameEvent<int> { }

[CreateAssetMenu(menuName = "EngineData/EventBus/Float Event")]
public class FloatGameEvent : BaseGameEvent<float> { }

[CreateAssetMenu(menuName = "EngineData/EventBus/String Event")]
public class StringGameEvent : BaseGameEvent<string> { }

[CreateAssetMenu(menuName = "EngineData/EventBus/Vector2 Event")]
public class Vector2GameEvent : BaseGameEvent<Vector2> { }

[CreateAssetMenu(menuName = "EngineData/EventBus/Bool Event")]
public class BoolGameEvent : BaseGameEvent<bool> { }