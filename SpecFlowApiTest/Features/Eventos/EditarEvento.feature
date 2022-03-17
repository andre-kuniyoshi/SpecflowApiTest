#language: pt-br

Funcionalidade: Editar evento
	Como Usuário/Administrador 
	Quero poder editar um evento já agendado
	Para que suas informacoes sejam alterados na agenda da Vaivoa

Contexto:
	Dado que estou autenticado no sistema
	E a rota do endpoint é 'eventos' e o método http é 'Put'

Cenario: Editar um evento com dados validos
	Dado que tenho um evento já agendado para o dia '20' para ser 'editado'
	E que quero editar o evento com dados validos
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NoContent'

Cenario: Editar um evento passando um tipo evento que nao existe
	Dado que tenho um evento já agendado para o dia '21' para ser 'editado'
	E que quero editar o evento passando um tipo que nao existe
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadResquest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Editar um evento que não foi vc que cadastrou
	Dado que tenho um evento já agendado para o dia '22' para ser 'editado'
	E que quero editar o evento passando um tipo que nao existe
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadResquest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Editar um evento que já foi finalizado
	Dado que tenho um evento já agendado para o dia '23' para ser 'editado'
	E que esse evento ja foi finalizdo
	E que quero editar o evento com dados validos
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadResquest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Editar um evento que não existe na agenda
	Dado que quero editar uma evento que não existe na agenda
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadResquest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Editar um evento para um horario ja reservado
	Dado que tenho um evento já agendado para o dia '25' para ser 'editado'
	E que quero editar o evento para um horario já ocupado
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadResquest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Editar um evento que não pertence ao usuario que criou
	Dado que tenho um evento já agendado para o dia '24' para ser 'editado'
	E que quero 'editar' esse evento com usuario diferente do que criou
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'Forbidden'

