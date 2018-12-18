#include <iostream>
using namespace std;

#include "Aspect.h"

class Action
{
public:
	void Say(const char* str)
	{
		std::cout << "Action::Say( " << str << " )" << std::endl;
	}

	void Sing(const char* str)
	{
		std::cout << "Action::Sing( " << str << " )" << std::endl;
	}
};


int main(int argc, char* argv[])
{
	Action a;
	LogAspect<Action>(&a, "Action::Say")->Say("Hi");
	LogAspect<Action>(&a, "Action::Sing")->Sing("Hi");
	std::cout << std::endl;

	LockAspect<Action>(&a)->Say("hello");

	while (1);
	return 0;
}
