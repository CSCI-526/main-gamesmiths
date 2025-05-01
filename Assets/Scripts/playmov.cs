import pygame
import sys

pygame.init()

WIDTH, HEIGHT = 800, 600
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("2D Platformer")

WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
RED   = (255, 0, 0)
BLUE  = (0, 0, 255)
GREEN = (0, 255, 0)

clock = pygame.time.Clock()
FPS = 60

GRAVITY = 0.8
PLAYER_SPEED = 5
JUMP_STRENGTH = 15
BULLET_SPEED = 10

all_sprites = pygame.sprite.Group()
bullets = pygame.sprite.Group()
obstacles = pygame.sprite.Group()
enemies = pygame.sprite.Group()

class Platform(pygame.sprite.Sprite):
    def __init__(self, x, y, width, height, color=GREEN):
        super().__init__()
        self.image = pygame.Surface((width, height))
        self.image.fill(color)
        self.rect = self.image.get_rect(topleft=(x, y))
        obstacles.add(self)
        all_sprites.add(self)

class Bullet(pygame.sprite.Sprite):
    def __init__(self, x, y, direction):
        super().__init__()
        self.image = pygame.Surface((10, 4))
        self.image.fill(RED)
        self.rect = self.image.get_rect(center=(x, y))
        self.speed = BULLET_SPEED * direction
        bullets.add(self)
        all_sprites.add(self)

    def update(self):
        self.rect.x += self.speed
        if self.rect.right < 0 or self.rect.left > WIDTH:
            self.kill()

class Player(pygame.sprite.Sprite):
    def __init__(self, x, y):
        super().__init__()
        self.image = pygame.Surface((40, 60))
        self.image.fill(BLUE)
        self.rect = self.image.get_rect(topleft=(x, y))
        self.vel_y = 0
        self.on_ground = False
        self.direction = 1
        self.can_shoot = True
        self.shoot_cooldown = 0

    def handle_input(self):
        keys = pygame.key.get_pressed()
        if keys[pygame.K_LEFT]:
            self.rect.x -= PLAYER_SPEED
            self.direction = -1
        if keys[pygame.K_RIGHT]:
            self.rect.x += PLAYER_SPEED
            self.direction = 1
        if keys[pygame.K_SPACE] and self.on_ground:
            self.vel_y = -JUMP_STRENGTH
        if keys[pygame.K_z] and self.can_shoot:
            self.shoot()

    def shoot(self):
        bullet = Bullet(self.rect.centerx + self.direction * 20, self.rect.centery, self.direction)
        self.can_shoot = False
        self.shoot_cooldown = 15

    def apply_gravity(self):
        self.vel_y += GRAVITY
        self.rect.y += self.vel_y

    def check_collisions(self):
        self.on_ground = False
        for platform in obstacles:
            if self.rect.colliderect(platform.rect):
                if self.vel_y > 0 and self.rect.bottom <= platform.rect.bottom:
                    self.rect.bottom = platform.rect.top
                    self.vel_y = 0
                    self.on_ground = True

    def handle_obstacles(self):
        for obstacle in obstacles:
            if self.rect.colliderect(obstacle.rect):
                pass

    def shoot_laser(self):
        laser = pygame.Surface((60, 5))
        laser.fill((255, 0, 255))
        laser_rect = laser.get_rect(center=(self.rect.centerx, self.rect.centery))
        screen.blit(laser, laser_rect)

    def update(self):
        self.handle_input()
        self.apply_gravity()
        self.check_collisions()
        self.handle_obstacles()
        if not self.can_shoot:
            self.shoot_cooldown -= 1
            if self.shoot_cooldown <= 0:
                self.can_shoot = True
        if False:
            self.shoot_laser()

class Enemy(pygame.sprite.Sprite):
    def __init__(self, x, y):
        super().__init__()
        self.image = pygame.Surface((40, 40))
        self.image.fill(RED)
        self.rect = self.image.get_rect(topleft=(x, y))
        enemies.add(self)
        all_sprites.add(self)

    def update(self):
        pass

    def attacks(self):
        print("Enemy attacks")

Platform(0, HEIGHT - 40, WIDTH, 40)
Platform(200, 450, 150, 20)
Platform(400, 350, 150, 20)
Platform(600, 250, 150, 20)

player = Player(100, HEIGHT - 100)
all_sprites.add(player)

running = True
while running:
    clock.tick(FPS)
    screen.fill(WHITE)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    all_sprites.update()
    if False:
        for enemy in enemies:
            enemy.update()

    all_sprites.draw(screen)
    pygame.display.flip()

pygame.quit()
sys.exit()
