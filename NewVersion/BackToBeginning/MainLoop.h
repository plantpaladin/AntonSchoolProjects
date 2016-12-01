#pragma once
#include "SFML\Graphics\Texture.hpp"
#include "Object.h"
#include <vector>
#include "SFML\Graphics\RenderWindow.hpp"
#include "Enemy.h"
#include "Shot.h"
#include "Player.h"
#include <sstream>
#include "SFML\Graphics\Text.hpp"

class MainLoop
{
public:
	MainLoop();
	void update(float deltaTime);
	bool collide();//put in a separate function to reduce size of update function
	void render(sf::RenderWindow * window);
	~MainLoop();

private:
	float m_timeSinceLastSpawn;
	int m_score;
	int m_lives;
	sf::Texture m_enemyTexture;
	sf::Texture m_shotTexture;
	sf::Texture m_enemyShotTexture;
	sf::Texture m_playerTexture;
	std::vector<Shot *> m_activePlayerShots;
	std::vector<Shot *> m_poolPlayerShots;
	std::vector<Shot *> m_activeEnemyShots;
	std::vector<Shot *> m_poolEnemyShots;
	std::vector<Enemy *> m_activeEnemies;
	std::vector<Enemy *> m_poolEnemies;
	sf::Font m_font;
	Player * m_player;
	std::string m_livesDisplay;
	std::string m_scoreDisplay;
	sf::Text m_sfScore;
	sf::Text m_sfLives;
	
};

