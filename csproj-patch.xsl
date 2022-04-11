<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    version="2.0">

	<!-- The output will be an indented XML file -->
    <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />

	<!-- (T1) -->
	<!-- Template for the root element -->
    <xsl:template match="/">
		<Project>
			<xsl:copy-of select="Project/@*" />
			<xsl:apply-templates select="Project/node()" />
		</Project>
    </xsl:template>
	
	<xsl:template match="node()|@*" priority="1.5">
        <xsl:copy>
			<xsl:apply-templates select="node()|@*" />
		</xsl:copy>
    </xsl:template>
	
	<xsl:template match="/Project/ItemGroup/ProjectReference" priority="1.8">
        <xsl:copy>
			<xsl:apply-templates select="@*" />
		</xsl:copy>
    </xsl:template>
	
</xsl:stylesheet>
