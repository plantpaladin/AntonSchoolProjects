#include "MainLoop.h"
#include <iostream>
#include "Player.h"
#include "SFML\Window.hpp"
#include <stdlib.h>
#include <time.h>
static int maxEnemyShips = 5;
static int maxEnemyShots = 10;
static int maxPlayerShots = 5;
static int enemyRadius = 64;
static int shipRadius = 64;
static int shotRadius = 8;
static float shotSpeed = 180.0;//movespeed is floats because they depend on deltatime
static float shipSpeed = 90.0;
static float spawnSpeed = 5.0;
static float firingSpeed = 10.0;

static int maxX = 576;//the width of the screen minus the radius of the ship
static float maxY = 960;//the height of the screen
MainLoop::MainLoop()
{
	srand(time(NULL));
	m_font.loadFromFile("minecraft.ttf");
	m_sfScore = sf::Text();
	m_sfScore.setFont(m_font);
	m_sfScore.setCharacterSize(24);
	m_sfLives = sf::Text();
	m_sfLives.setPosition(0, 20);
	m_sfLives.setFont(m_font);
	m_sfLives.setCharacterSize(24);
	m_timeSinceLastSpawn = 0;
	m_score = 0;
	m_lives = 10;
	m_enemyTexture.loadFromFile("alien.png");
	m_playerTexture.loadFromFile("ship.png");
	m_shotTexture.loadFromFile("shot.png");
	m_enemyShotTexture.loadFromFile("enemyShot.png"); 
	sf::Sprite * playerSprite = new sf::Sprite(m_playerTexture);//as the sprite is created via new it will not go out of scope
	m_player = new Player(300, 800, 64, playerSprite);
	for (int i = 0; i < maxEnemyShips; i++)
	{
		sf::Sprite * newSprite = new sf::Sprite(m_enemyTexture);
		m_poolEnemies.push_back(new Enemy(0, 0, 64,shipSpeed, newSprite));
	}
	for (int i = 0; i < maxEnemyShots; i++)
	{
		sf::Sprite * newSprite = new sf::Sprite(m_enemyShotTexture);
		m_poolEnemyShots.push_back(new Shot(0, 0, 8, shotSpeed, newSprite));
	}
	for (int i = 0; i < maxPlayerShots; i++)
	{
		sf::Sprite * newSprite = new sf::Sprite(m_shotTexture);
		m_poolPlayerShots.push_back(new Shot(0, 0, 8,0 -shotSpeed, newSprite));
	}

}

void MainLoop::update(float deltaTime)
{

	for (int i = 0; i < m_activeEnemies.size(); i++)
	{
		Enemy * selectedEnemy = m_activeEnemies[i];
		selectedEnemy->update(deltaTime);
		if (selectedEnemy->getPosition().y>maxY)
		{
			m_poolEnemies.push_back(selectedEnemy);
			m_activeEnemies.erase(m_activeEnemies.begin()+i);

		}
		else if (selectedEnemy->canShoot()==true && m_poolEnemyShots.empty()==false)
		{
			Shot * activatedShot = m_poolEnemyShots.back();
			sf::Vector2f newPosition = selectedEnemy->getPosition();
			newPosition.x += enemyRadius;
			newPosition.y += enemyRadius;
			activatedShot->newPosition(newPosition);
			m_activeEnemyShots.push_back(activatedShot);
			m_poolEnemyShots.pop_back();
		}
	}
	m_timeSinceLastSpawn += deltaTime;
	if (m_timeSinceLastSpawn > spawnSpeed && (m_poolEnemies.empty() == false))
	{//this segment moves a player from the pool to the game
		m_timeSinceLastSpawn -= spawnSpeed;
		Enemy * activatedEnemy = m_poolEnemies.back();
		activatedEnemy->newPosition(25);
		m_activeEnemies.push_back(activatedEnemy);
		m_poolEnemies.pop_back();
	}
	m_player->update(deltaTime);
	if (m_player->isShooting() && m_poolPlayerShots.empty() == false)
	{
		Shot * activatedShot = m_poolPlayerShots.back();
		sf::Vector2f newPosition = m_player->getPosition();
		newPosition.x += enemyRadius;
		newPosition.y += enemyRadius;
		activatedShot->newPosition(newPosition);
		m_activePlayerShots.push_back(activatedShot);
		m_poolPlayerShots.pop_back();
	}
	for (int i = 0; i < m_activeEnemyShots.size(); i++)
	{
		Shot * selectedShot = m_activeEnemyShots[i];

		selectedShot->update(deltaTime);
		if (selectedShot->getPosition().y<0)
		{
			m_poolEnemyShots.push_back(selectedShot);
			m_activeEnemyShots.erase(m_activeEnemyShots.begin() + i);
		}
	}
	for (int i = 0; i < m_activePlayerShots.size(); i++)
	{
		Shot * selectedShot = m_activePlayerShots[i];
		selectedShot->update(deltaTime);
		if (selectedShot->getPosition().y<0)
		{
			m_poolPlayerShots.push_back(selectedShot);
			m_activePlayerShots.erase(m_activePlayerShots.begin() + i);
		}
	}
	
}
bool MainLoop::collide()
{
	for (int i = 0; i < m_activeEnemies.size(); i++)
	{
		Enemy * selectedEnemy = m_activeEnemies[i];
		for (int j = 0; i < m_activePlayerShots.size(); i++)
		{
			Shot * selectedShot = m_activePlayerShots[j];
			if (selectedShot->collide(selectedEnemy->getSprite())==true)
			{
				m_score += 10;
				m_poolEnemies.push_back(selectedEnemy);
				m_activeEnemies.erase(m_activeEnemies.begin() + i);
  				m_poolPlayerShots.push_back(selectedShot);
				m_activePlayerShots.erase(m_activePlayerShots.begin() + j);
				break;
			}
			
			
		}
	}//I use a second looop so I dont have to use goto to break out of loop and because if an enemy was shot down then
	//the game shouldnt check if it collide with the player

	for (int i = 0; i < m_activeEnemies.size(); i++)
	{
		Enemy * selectedEnemy = m_activeEnemies[i];
		if (m_player->collide(selectedEnemy->getSprite()) == true)
		{
			m_score += 10;
			m_poolEnemies.push_back(selectedEnemy);
			m_activeEnemies.erase(m_activeEnemies.begin() + i);
			m_lives -= 1;
			if (m_lives < 1)
			{
				return true;
			}
		}

	}
	for (int i = 0; i < m_activeEnemyShots.size(); i++)
	{
		Shot * selectedShot = m_activeEnemyShots[i];
		if (m_player->collide(selectedShot->getSprite()) == true)
		{
			m_poolEnemyShots.push_back(selectedShot);
			m_activeEnemyShots.erase(m_activeEnemyShots.begin() + i);
			m_lives -= 1;
			if (m_lives < 1)
			{
				return true;
			}
		}

	}
	
	return false;
}
void MainLoop::render(sf::RenderWindow * window)
{
	for (int i = 0; i < m_activeEnemies.size(); i++)
	{
		window->draw(*m_activeEnemies[i]->getSprite());
	}
	for (int i = 0; i < m_activeEnemyShots.size(); i++)
	{
		window->draw(*m_activeEnemyShots[i]->getSprite());
	}
	for (int i = 0; i < m_activePlayerShots.size(); i++)
	{
		window->draw(*m_activePlayerShots[i]->getSprite());
	}
	window->draw(*m_player->getSprite());

	m_livesDisplay = "Lives ";//the value of the string must be reset
	m_scoreDisplay = "Score ";
	m_scoreDisplay += std::to_string(m_score);
	m_livesDisplay += std::to_string(m_lives);


	m_sfScore.setString(m_scoreDisplay);
	m_sfLives.setString(m_livesDisplay);
	window->draw(m_sfScore);
	window->draw(m_sfLives);
}
MainLoop::~MainLoop()
{
}
