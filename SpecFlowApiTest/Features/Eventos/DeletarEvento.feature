#language: pt-br

Funcionalidade: Deletar evento
	Como Usuário/Administrador 
	Quero poder deletar um evento já agendado
	Para que seja retirado da agenda de eventos da Vaivoa

Contexto:
	Dado que estou autenticado no sistema
	E a rota do endpoint é 'eventos' e o método http é 'Delete'

Cenario: Deletar um evento agendado
	Dado que tenho um evento já agendado para o dia '15' para ser 'deletado'
	E quero deletar esse evento
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NoContent'
	E esse evento não existe mais na agenda de eventos da vaivoa

Cenario: Deletar um evento que nao esta na lista de eventos
	Dado que tenho um evento já agendado para o dia '16' para ser 'deletado'
	E quero tentar deletar esse evento passando id errado
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NotFound'
	E esse evento ainda está na agenda de eventos da vaivoa

Cenario: Deletar um evento que não pertence ao usuario que criou
	Dado que tenho um evento já agendado para o dia '17' para ser 'deletado'
	E que quero 'deletar' esse evento com usuario diferente do que criou
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'Forbidden'
