// NAMING
// | Favor readability over brevity. 
// | Variable names should be descriptive, clear, and unambiguous because they represent a thing or state.
// | Use a noun when naming them except when the variable is of the type bool.
// | Prefix Booleans with a verb to make their meaning more apparent. e.g. isDead, isWalking, hasDamageMultiplier.
// | Choose identifier names that are easily readable. 
// | For example, a property named HorizontalAlignment is more readable than AlignmentHorizontal.
// | Make type names unambiguous across namespaces and problem domains by avoiding common terms 
// | or adding a prefix or a suffix. (ex. use 'PhysicsSolver', not 'Solver')

// CASING AND PREFIXES:
// | Use Pascal case (e.g. ExamplePlayerController, MaxHealth, etc.) unless noted otherwise.
// | Use camel case (e.g. examplePlayerController, maxHealth, etc.) for local/private variables and parameters.
// | Avoid redundant names: If your class is called Player, you don’t need to create member variables called PlayerScore or PlayerTarget. 
// | Trim them down to Score or Target.

// FORMATTING:
// | Allman (opening curly braces on a new line) style braces.

// COMMENTS:
// | Comment when needed. That is when the code isn’t self-explanatory and needs clarification beyond good naming revealing the intent.
// | Use a Tooltip instead of a comment for serialized fields if your fields in the Inspector need explanation.
// | Rather than using Regions think of them as a code smell indicating your class is too large and needs refactoring. 

// | CLASS ORGANIZATION:
// | Organize your class in the following order: Fields, Properties, Events, 
// | Monobehaviour methods (Awake, Start, OnEnable, OnDisable, OnDestroy, etc.), public methods, private, methods, other Classes.
// | Use of #region is generally discouraged as it can hide complexity and make it harder to read the code.

// USING LINES:
// | Keep using lines at the top of your file.
// | Remove unsed lines.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// NAMESPACES:
// | Use Pascal case, without special symbols or underscores.
// | Create sub-namespaces with the dot (.) operator, e.g. MyApplication.GameFlow, MyApplication.AI, etc.

namespace UnityCSharpStyleSheetExample
{
    // ENUMS:
    // | Use enums when an object or action can only have one value at a time.
    // | Use Pascal case for enum names and values.
    // | Use a singular noun for the enum name as it represents a single value from a set of possible values. 
    // | They should have no prefix or suffix.
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    // FLAG ENUM:
    // | Use a plural noun to indicate the possibility of multiple selections (e.g., AttackModes). 
    // | No prefix or suffix.
    // | Use column-alignment for binary values.
    // | Alternatively, consider using the 1 << butnum style.
    [Flags]
    public enum AttackModes
    {
        // Decimal                         // Binary
        None = 0,                          // 000000
        Melee = 1,                         // 000001
        Ranged = 2,                        // 000010
        Special = 4,                       // 000100

        MeleeAndSpecial = Melee | Special  // 000101
    }

    // INTERFACES:
    // | Prefix interface names with a capital I
    // | Follow this with naming interfaces with adjective phrases that describe the functionality.
     
    public interface IDamageable
    {
        string DamageTypeName { get; }
        float DamageValue { get; }

        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    public interface IDamageable<T>
    {
        void Damage(T damageTaken);
    }

    // CLASSES or STRUCTS:
    // | Use Pascal case 
    // | Name them with nouns or noun phrases.
    // | Avoid prefixes.
    // | One Monobehaviour per file. If you have a Monobehaviour in a file, the source file name must match. 
    public class StyleExample : MonoBehaviour
    {

        // FIELDS: 
        // | Avoid special characters (backslashes, symbols, Unicode characters); these can interfere with command line tools.
        // | Use nouns for names, but prefix booleans with a verb.
        // | Use Pascal case for public fields. Use camel case for private variables.
        
        private int elapsedTimeInHours;

        // Booleans ask a question that can be answered true or false.
        [SerializeField] bool isPlayerDead;

        // This groups data from the custom PlayerStats class in the Inspector.
        [Serializable] PlayerStats stats;

        // This limits the values to a Range and creates a slider in the Inspector.
        [Range(0f, 1f)] [SerializeField] float rangedStat;

        // A tooltip can replace a comment on a serialized field and do double duty.
        [Tooltip("This is another statistic for the player.")]
        [SerializeField] float anotherStat;

        // PROPERTIES:
        // | Preferable to a public field.
        // | Pascal case, without special characters.
        // | Use the expression-bodied properties to shorten, but choose your preferrred format.
        // | E.g. use expression-bodied for read-only properties but { get; set; } for everything else.
        // | Use the Auto-Implementated Property for a public property without a backing field.
        // | For get or set operations involving complex logic or computation, use methods instead of properties.

        // The private backing field
        int maxHealth;

        // Read-only, returns backing field
        public int MaxHealthReadOnly => maxHealth;

        // Equivalent to: 
        // public int MaxHealth { get; private set; }

        // explicitly implementing getter and setter
        public int MaxHealth
        {
            get => m_maxHealth;
            set => m_maxHealth = value;
        }

        // Write-only (not using backing field)
        public int Health { private get; set; }

        // Write-only, without an explicit setter
        public void SetMaxHealth(int newMaxValue) => m_maxHealth = newMaxValue;

        // Auto-implemented property without backing field
        public string DescriptionName { get; set; } = "Fireball";

        // EVENTS:
        // | Name with a verb phrase.
        // | Use System.Action delegate for most events (can take 0 to 16 parameters).
        // | Choose a naming scheme for events, event handling methods (subscriber/observer), 
        // | and event raising methods (publisher/subject)
        // | e.g. event/action = "OpeningDoor", event raising method = "OnDoorOpened", event handling method = "MySubject_DoorOpened"
   
        // Event before
        public event Action OpeningDoor;

        // Event after
        public event Action DoorOpened;     

        // Event with int parameter
        public event Action<int> PointsScored;
        
        // Custom event with custom EventArgs
        public event Action<CustomEventArgs> ThingHappened;

        // These are event raising methods, e.g. OnDoorOpened, OnPointsScored, etc.
        // Prefix the event raising method (in the subject) with “On”.
        // Alternatively, event handling method e.g. MySubject_DoorOpened().
        
        public void OnDoorOpened()
        {
            DoorOpened?.Invoke();
        }

        public void OnPointsScored(int points)
        {
            PointsScored?.Invoke(points);
        }

        // This is a custom EventArg made from a struct.
        public struct CustomEventArgs
        {
            public int ObjectID { get; }
            public Color Color { get; }

            public CustomEventArgs(int objectId, Color color)
            {
                this.ObjectID = objectId;
                this.Color = color;
            }
        }

        // METHODS:
        // | Start a method name with a verb or verb phrases to show an action. Add context if necessary. e.g. GetDirection, FindTarget, etc.
        // | Methods returning bool should ask questions: Much like Boolean variables themselves.
        // | Prefix methods with a verb if they return a true-false condition. 
        // | This phrases them in the form of a question, e.g. IsGameOver, HasStartedTurn
        // | Use camel case for parameters. Format parameters passed into the method like local variables.

        // Methods start with a verb.
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Methods ask a question when they return bool.
        public bool IsNewPosition(Vector3 newPosition)
        {
            return (transform.position == newPosition);
        }

        void FormatExamples(int someExpression)
        {

            var powerUps = new List<PlayerStats>();
            var dict = new Dictionary<string, List<GameObject>>();

            // BRACES: 
            // | Where possible, don’t omit braces, even for single-line statements. 
            // | Or avoid single-line statements entirely for debuggability.
            // | Keep braces in nested multi-line statements.

            // This single-line statement keeps the braces...
            for (int i = 0; i < 100; i++) { DoSomething(i); }

            // ... but this is more reable and often more debuggable. 
            for (int i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            // Separate the statements for readability.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }
        }

        void DoSomething(int x)
        {
            // .. 
        }
    }

    // OTHER CLASSES:
    // | Define as many other helper/non-Monobehaviour classes in your file as needed.
    // | This is a serializable class that groups fields in the Inspector.
    [Serializable]
    public struct PlayerStats
    {
        public int MovementSpeed;
        public int HitPoints;
        public bool HasHealthPotion;
    }

}
