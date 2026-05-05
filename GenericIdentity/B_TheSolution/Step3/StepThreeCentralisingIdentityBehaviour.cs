using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step3;

[DocFile]
[DocContent(
"""
Nu kunnen we ook een eenvoudige *base class* maken die deze functionaliteit verenigt, want:
- Een `Student`  **is** een entiteit.
- Een `Course`  **is** een entiteit. 
""")]
[DocHeader("Domain Entity")]
[DocExample(typeof(DomainEntity<>))]
[DocContent("Dan kunnen we het gedrag van *Id equality* centraliseren en zowel `Student` als `Course` laten overerven van `DomainEntity`:")]
[DocExample(typeof(Course))]
[DocExample(typeof(Student))]
[DocContent("Geen wijzigingen nodig in de service.")]
public class StepThreeCentralisingIdentityBehaviour
{

}
