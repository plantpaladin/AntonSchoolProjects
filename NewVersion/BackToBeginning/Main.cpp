#include <SFML/Window.hpp>
#include "MainLoop.h"
#include <Windows.h>
int main()
{
	sf::RenderWindow * window = new sf::RenderWindow(sf::VideoMode(640, 960), "Game On");
	
	MainLoop * gameLoop = new MainLoop;
	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	sf::Clock deltaClock;
	sf::Time deltaTime;

	// run the program as long as the window is open
	while (window->isOpen())
	{
		// check all the window's events that were triggered since the last iteration of the loop
		sf::Event event;
		while (window->pollEvent(event))
		{
			// "close requested" event: we close the window
			if (event.type == sf::Event::Closed)
				window->close();
			

		}
		
		gameLoop->update(deltaTime.asSeconds());
		if (gameLoop->collide()==true)//returns true if the player dies
		{
			return 0;
		}
			window->clear(sf::Color::Blue);
			gameLoop->render(window);
			window->display();
			deltaTime = deltaClock.restart();
	}

	return 0;
}
