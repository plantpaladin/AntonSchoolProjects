#include "player.h"
#include "SFML\Window.hpp"
static float playerSpeed = 180;
static float firingSpeed = 2.0;
static float maxX = 512;//the width of the screen minus the radius of the ship
static float maxY = 832;
Player::Player(float x, float y, float radius,sf::Sprite * sprite)
{
	m_x = x;
	m_y = y;
	m_radius = radius;
	m_sprite = sprite;
	m_speed = playerSpeed;
	m_timeSincelastShot = firingSpeed;
}

void Player::update(float deltaTime)
{
	if (m_timeSincelastShot < firingSpeed)//just a habit of not letting values overflow
	{
		m_timeSincelastShot += deltaTime;
	}
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
	{
		float nextX = m_x + m_speed*deltaTime;
		if (nextX > maxX)
		{
			m_x = maxX;
		}
		else
		{
			m_x = nextX;
		}
	}
	else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
	{
		float nextX = m_x - m_speed*deltaTime;
		if (nextX < 0)
		{
			m_x = 0;
		}
		else
		{
			m_x = nextX;
		}
	}
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Down))
	{
		float nexty = m_y + m_speed*deltaTime;
		if (nexty > maxY)
		{
			m_y = maxY;
		}
		else
		{
			m_y = nexty;
		}
	}
	else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up))
	{
		float nexty = m_y - m_speed*deltaTime;
		if (nexty < m_radius*2)
		{
			m_y = m_radius*2;
		}
		else
		{
			m_y = nexty;
		}
	}
}

bool Player::isShooting()
{
	if ((m_timeSincelastShot >= firingSpeed)&&(sf::Keyboard::isKeyPressed(sf::Keyboard::Space)))
	{
		m_timeSincelastShot = 0;
		return true;
	}
	return false;
}



Player::~Player()
{
	delete m_sprite;
}
