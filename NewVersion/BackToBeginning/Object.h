#pragma once
#include <math.h>
#include "SFML\Graphics\Sprite.hpp"
#include "SFML\Graphics\RenderWindow.hpp"
class Object
{
protected:
	int m_x, m_y, m_radius;
	sf::Sprite m_sprite;//stored as a pointer so it can be retrived
public:
	virtual void update() = 0;//unique for all players, primarly movement
	virtual bool collide(double x, double y, double radius)//same for all objects
	{//returns whether the player collides
		double dx = x - m_x;
		double dy = y - m_y;
		double distance = sqrt(dx*dx + dy*dy);
		if (distance < (radius + m_radius))
		{
			return true;
		}
		return false;
	}
	virtual void drawSprite(sf::RenderWindow window)
	{
		window.draw(m_sprite);
	}
};

