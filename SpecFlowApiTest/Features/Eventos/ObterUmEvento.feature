#language: pt-br

Funcionalidade: Obter um evento da agenda
	Como Usuário/Administrador 
	Quero buscar um evento na agenda
	Para obter informações detalhadas desse evento

Contexto:
	Dado que estou autenticado no sistema
	E a rota do endpoint é 'eventos' e o método http é 'Get'

Cenario: Obter um evento existente na agenda da Vaivoa com seus dados detalhados
	Dado que tenho um evento já agendado para o dia '01' para ser 'obtido'
	E que quero buscar um evento existente na agenda da Vaivoa
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'Ok'
	E com o campo sucesso do body da resposta igual a 'true'

Cenario: Tentar obter um evento que não existe na agenda
	Dado que tenho um evento já agendado para o dia '02' para ser 'obtido'
	E que quero tentar obter um evento inexistente na agenda da Vaivoa
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'NotFound'
