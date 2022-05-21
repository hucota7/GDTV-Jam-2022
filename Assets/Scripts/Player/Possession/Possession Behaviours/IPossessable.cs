public interface IPossessable {
	Entity GetEntity();
	void Possess(IPossessable previouslyPossessed);
	void Unpossess(IPossessable newlyPossessed);
}
