#language: pt-br

Funcionalidade: Confirmar realizacao de evento
	Como Usuário/Administrador 
	Quero confirmar um evento realizado
	Para que seja confirmado a sua realizacao na agenda da Vaivoa

Contexto:
	Dado que estou autenticado no sistema
	E a rota do endpoint é 'eventos' e o método http é 'Patch'

Cenario: Confirmar a realizacao de um evento da agenda
	Dado que tenho um evento já agendado para o dia '05' para ser 'confirmado'
	E quero finalizar esse evento
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NoContent'

Cenario: Confirmar a realizacao de um evento ja finalizado
	Dado que tenho um evento já agendado para o dia '06' para ser 'confirmado'
	E que esse evento ja foi finalizdo
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NotFound'

Cenario: Confirmar a realizacao de um evento com id inexistente
	Dado que tenho um evento já agendado para o dia '07' para ser 'confirmado'
	E quero tentar finalizr com um id inexistente
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NotFound'

Cenario: Confirmar a realizacao de um evento com link invalido
	Dado que tenho um evento já agendado para o dia '08' para ser 'confirmado'
	E quero tentar finalizr com um link invalido
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadRequest'

Cenario: Confirmar a realizacao de um evento que nao pertence ao usuario
	Dado que tenho um evento já agendado para o dia '09' para ser 'confirmado'
	E quero finalizar esse evento
	E  que quero 'finalizar' esse evento com usuario diferente do que criou
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'Forbidden'

