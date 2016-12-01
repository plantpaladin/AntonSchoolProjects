#include "Shot.h"



Shot::Shot(float x, float y, float radius, float speed, sf::Sprite * sprite)
{
	m_x = x;
	m_y = y;
	m_radius = radius;
	m_speed = speed;
	m_sprite = sprite;
}

void Shot::newPosition(sf::Vector2f newPosition)
{
	m_x = newPosition.x;
	m_y = newPosition.y;
}

void Shot::update(float deltaTime)
{
	m_y += m_speed*deltaTime;
}

Shot::~Shot()
{
	delete m_sprite;
}
