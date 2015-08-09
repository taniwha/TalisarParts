KSPDIR		:= ${HOME}/ksp/KSP_linux
MANAGED		:= ${KSPDIR}/KSP_Data/Managed
GAMEDATA	:= ${KSPDIR}/GameData
TPGAMEDATA  := ${GAMEDATA}/TalisarParts
PLUGINDIR	:= ${TPGAMEDATA}/Plugins

RESGEN2	:= resgen2
GMCS	:= gmcs
GIT		:= git
TAR		:= tar
ZIP		:= zip

all:

clean:

info:
	@echo "Talisar Parts Build Information"
	@echo "    resgen2:  ${RESGEN2}"
	@echo "    gmcs:     ${GMCS}"
	@echo "    git:      ${GIT}"
	@echo "    tar:      ${TAR}"
	@echo "    zip:      ${ZIP}"
	@echo "    KSP Data: ${KSPDIR}"
	@echo "    Plugin:   ${PLUGINDIR}"

zip:
	VERSION=`tools/git-version.sh`; \
		cd GameData; \
		${ZIP} -r9 ../TalisarParts-$${VERSION}.zip TalisarParts

.PHONY: all clean info
